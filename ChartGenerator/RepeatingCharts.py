import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cbook as cbook

files = ["esc16j", "chr12a", "tai15b", "tai20b", "els19", "esc16a", "tai15a"]


def read_file(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n;") for line in f.readlines()]
    for line in lines:
        data.append(line.split(','))
    return data


def convert_data(data):
    data_to_chart_x = []
    data_to_chart_average = []
    data_to_chart_best = []
    i = 0
    for x in data:
        if i>150:
            break
        i += 1
        data_to_chart_x.append(i)
        data_to_chart_average.append(int(x[0]) / int(x[2]))
        data_to_chart_best.append(int(x[0]) / int(x[3]))
    return data_to_chart_x, data_to_chart_average, data_to_chart_best


def create_chart(greedy_data, steepest_data, filename):
    g_data_x, g_data_av, g_data_b = convert_data(greedy_data)
    s_data_x, s_data_av, s_data_b = convert_data(steepest_data)
    fig, ax = plt.subplots()
    plt.ylabel('jakość')
    plt.xlabel('liczba powtórzeń')
    plt.scatter(g_data_x, g_data_b,
                s=np.ones(len(g_data_x)) * 4,
                c='red',
                label='greedy najlepszy',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(g_data_x, g_data_av,
                s=np.ones(len(g_data_x)) * 4,
                c='orange',
                label='greedy średni',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(s_data_x, s_data_b,
                s=np.ones(len(g_data_x)) * 4,
                c='blue',
                label='steepest najlepszy',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(s_data_x, s_data_av,
                s=np.ones(len(g_data_x)) * 4,
                c='green',
                label='steepest średni',
                alpha=0.9,
                edgecolors='none')
    ax.legend()
    ax.grid(True)
   # plt.savefig(filename + ".svg")
    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")


for file in files:
    steepest_data = read_file('../AlgBattle/steepest_repeating_' + file + '.csv')
    greedy_data = read_file('../AlgBattle/greedy_repeating_' + file + '.csv')
    create_chart(greedy_data, steepest_data, file)
