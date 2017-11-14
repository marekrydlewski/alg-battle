import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cbook as cbook

#files = ["tai15b", "tai20b", "tai25b", "tai30b", "bur26f", "chr12b", "chr20a"]
files = ["bur26a", "chr12a", "chr20b", "els19", "esc16a", "esc16j"]
#files = ['../AlgBattle/steepest_firstVsLastsResult_chr12b.csv',
 #        '../AlgBattle/greedyfirstVsLastsResult_chr12b.csv']


def read_file(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n;") for line in f.readlines()]
    for line in lines:
        data.append(line.split(','))
    return data


def convert_data(data):
    data_to_chart_x = []
    data_to_chart_y = []
    for x in data:
        data_to_chart_x.append(int(x[0])/int(x[1]))
        data_to_chart_y.append(int(x[0])/int(x[2]))
    return data_to_chart_x, data_to_chart_y


def create_chart(greedy_data, steepest_data, filename):
    g_data_x, g_data_y = convert_data(greedy_data)
    s_data_x, s_data_y = convert_data(steepest_data)
    fig, ax = plt.subplots()
    plt.xlabel('jakość początkowa')
    plt.ylabel('jakość końcowa')
    plt.scatter(g_data_x, g_data_y,
                s=np.ones(len(g_data_x)) * 2,
                c='red',
                label='greedy',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(s_data_x, s_data_y,
                s=np.ones(len(g_data_x)) * 2,
                c='blue',
                label='steepest',
                alpha=0.9,
                edgecolors='none')
    ax.legend()
    ax.grid(True)
    plt.savefig(filename + ".svg")
    plt.savefig(filename + ".png")


for file in files:
    steepest_data = read_file('../AlgBattle/steepest_firstVsLastsResult_' + file + '.csv' )
    greedy_data = read_file('../AlgBattle/greedy_firstVsLastsResult_' + file + '.csv')
    create_chart(greedy_data, steepest_data, file)