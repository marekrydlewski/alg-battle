import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cbook as cbook


def read_file(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n;") for line in f.readlines()]
    for line in lines:
        data.append(line.split(';'))
    return [list( map(float,i) ) for i in data]


def create_chart(data):
    x = [15, 20, 25, 30, 35, 40, 50, 60, 80]
    filename = "similarity"
    fig, ax = plt.subplots()
    plt.ylabel('podobieństwo')
    plt.xlabel('rozmiar instancji')

    plt.scatter(x, data[0],
                c='red',
                label='heuristic',
                alpha=0.9,
                marker="o",
                edgecolors='none')

    plt.scatter(x, data[1],
                c='red',
                label='random',
                alpha=0.9,
                marker="s",
                edgecolors='none')

    plt.scatter(x, data[2],
                c='green',
                label='greedy',
                alpha=0.9,
                marker="_",
                edgecolors='none')

    plt.scatter(x, data[3],
                c='green',
                label='steepest',
                alpha=0.9,
                marker="s",
                edgecolors='none')

    plt.scatter(x, data[4],
                c='gray',
                label='annealing',
                alpha=0.9,
                marker="|",
                edgecolors='none')


    plt.scatter(x, data[5],
                c='gray',
                label='tabu',
                alpha=0.9,
                marker="o",
                edgecolors='none')


    ax.legend(loc='center right',
              fancybox=False, shadow=False, ncol=1, bbox_to_anchor=(1.12, 0.65), fontsize='small', labelspacing=0.12)
    ax.grid(True)

    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")

d = read_file("../AlgBattle/taiOutput_score_similarity.csv")
create_chart(d)