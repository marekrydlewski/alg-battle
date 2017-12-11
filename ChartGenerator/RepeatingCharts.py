import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cbook as cbook

files = ["tai15a", "tai20b", "chr12a"]


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
        if i > 150:
            break
        i += 1
        data_to_chart_x.append(i)
        data_to_chart_average.append(int(x[0]) / int(x[2]))
        data_to_chart_best.append(int(x[0]) / int(x[3]))
    return data_to_chart_x, data_to_chart_average, data_to_chart_best


def create_chart(greedy_data, steepest_data, annealing_data, tabu_data, filename):
    g_data_x, g_data_av, g_data_b = convert_data(greedy_data)
    s_data_x, s_data_av, s_data_b = convert_data(steepest_data)
    a_data_x, a_data_av, a_data_b = convert_data(annealing_data)
    t_data_x, t_data_av, t_data_b = convert_data(tabu_data)
    fig, ax = plt.subplots()
    plt.ylabel('jakość')
    plt.xlabel('liczba powtórzeń')
    plt.scatter(g_data_x, g_data_b,
                s=np.ones(len(g_data_x)) * 6,
                c='red',
                label='greedy najlepszy',
                alpha=0.9,
                marker="*",
                edgecolors='none')
    plt.scatter(g_data_x, g_data_av,
                s=np.ones(len(g_data_x)) * 6,
                c='orange',
                label='greedy średni',
                alpha=0.9,
                marker="s",
                edgecolors='none')
    plt.scatter(s_data_x, s_data_b,
                s=np.ones(len(g_data_x)) * 6,
                c='blue',
                marker=".",
                label='steepest najlepszy',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(s_data_x, s_data_av,
                s=np.ones(len(g_data_x)) * 6,
                c='green',
                marker="o",
                label='steepest średni',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(a_data_x, a_data_b,
                s=np.ones(len(g_data_x)) * 6,
                c='brown',
                marker="o",
                label='annealing najlepszy',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(a_data_x, a_data_av,
                s=np.ones(len(g_data_x)) * 6,
                c='gray',
                marker="o",
                label='annealing średni',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(t_data_x, t_data_b,
                s=np.ones(len(g_data_x)) * 6,
                c='pink',
                marker="o",
                label='tabu najlepszy',
                alpha=0.9,
                edgecolors='none')
    plt.scatter(t_data_x, t_data_av,
                s=np.ones(len(g_data_x)) * 6,
                c='violet',
                marker="o",
                label='tabu średni',
                alpha=0.9,
                edgecolors='none')
    ax.legend()
    ax.grid(True)
    # plt.savefig(filename + ".svg")
    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")


for file in files:
    steepest_data = read_file('../AlgBattle/repeating_steepest_' + file + '.csv')
    greedy_data = read_file('../AlgBattle/repeating_greedy_' + file + '.csv')
    annealing_data = read_file('../AlgBattle/repeating_annealing_' + file + '.csv')
    tabu_data = read_file('../AlgBattle/repeating_tabu_' + file + '.csv')
    create_chart(greedy_data, steepest_data, annealing_data, tabu_data, file)
