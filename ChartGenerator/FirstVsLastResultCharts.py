import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cbook as cbook

files = ['../AlgBattle/greedyfirstVsLastsResult_bur26f.csv',
         '../AlgBattle/greedyfirstVsLastsResult_chr12b.csv']


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


def create_chart(data, filename):
    data_x,data_y = convert_data(data)

    plt.scatter(data_x, data_y, s=np.ones(len(data_x)), c=np.ones(len(data_x)), alpha=0.5)
    plt.show()



data = read_file(files[0])
create_chart(data, "first_vs_last_chart")