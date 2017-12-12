import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cbook as cbook

files = ["tai15b", "tai20b", "tai25b", "tai30b", "tai35b", "tai40b", "tai50b", "tai60b", "tai80b"]
instance_size = [15, 20, 25, 30, 35, 40, 50, 60, 80]
optimal_values = []


def read_optimal_solutions():
    data = []
    f = open('../AlgBattle/optimal_solutions.csv', 'r')
    lines = [line.rstrip("\n;") for line in f.readlines()]
    return lines


def convert_data(to_convert):
    result = []
    for i in range(0, len(to_convert)):
        if int(to_convert[i]) == 0:
            result.append(0)
        else:
            result.append(int(optimal_values[i]) / int(to_convert[i]))
    return result


def read_results_std(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n") for line in f.readlines()]
    for line in lines:
        splitted_line = line.split(';')
        splitted_line = [x for x in splitted_line if x != ""]
        data.append(splitted_line)

    heuristic = data[0]
    random = data[1]
    greedy = data[2]
    steepest = data[3]
    annealing = data[4]
    tabu = data[5]
    return list(map(float, heuristic)), list(map(float, random)), list(map(float, greedy)), list(
        map(float, steepest)), list(map(float, annealing)), list(map(float, tabu))


def read_results_time(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n") for line in f.readlines()]
    for line in lines:
        splitted_line = line.split(';')
        splitted_line = [x for x in splitted_line if x != ""]
        data.append(splitted_line)

    heuristic = [float(x) / 10 for x in data[0]]
    random = [float(x) / 10 for x in data[1]]
    greedy = [float(x) / 10 for x in data[2]]
    steepest = [float(x) / 10 for x in data[3]]
    annealing = [float(x) / 10 for x in data[4]]
    tabu = [float(x) / 10 for x in data[5]]
    return heuristic, random, greedy, steepest, annealing, tabu


def read_results_steps(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n") for line in f.readlines()]
    for line in lines:
        splitted_line = line.split(';')
        splitted_line = [x for x in splitted_line if x != ""]
        data.append(splitted_line)

    greedy = data[0]
    steepest = data[1]
    annealing = data[2]
    tabu = data[3]
    return greedy, steepest, annealing, tabu


def read_results(filename):
    data = []
    f = open(filename, 'r')
    lines = [line.rstrip("\n") for line in f.readlines()]
    for line in lines:
        splitted_line = line.split(';')
        splitted_line = [x for x in splitted_line if x != ""]
        data.append(splitted_line)

    heuristic = data[0]
    random = data[1]
    greedy = data[2]
    steepest = data[3]
    annealing = data[4]
    tabu = data[5]
    return convert_data(heuristic), convert_data(random), convert_data(greedy), convert_data(steepest), convert_data(
        annealing), convert_data(tabu)


def create_chart_steps(greedy_data, steepest_data, annealing_data, tabu_data,  filename, label):
    fig, ax = plt.subplots()
    plt.ylabel(label)
    plt.xlabel('rozmiar instancji')
    plt.scatter(instance_size, greedy_data,
                s=np.ones(len(greedy_data)) * 40,
                c='blue',
                marker="+",
                label='greedy',
                alpha=0.6,
                edgecolors='blue')
    plt.scatter(instance_size, steepest_data,
                s=np.ones(len(steepest_data)) * 40,
                c='green',
                marker="o",
                label='steepest',
                alpha=0.6,
                edgecolors='green')
    plt.scatter(instance_size, annealing_data,
                s=np.ones(len(annealing_data)) * 40,
                c='red',
                marker="o",
                label='annealing',
                alpha=0.6,
                edgecolors='red')
    plt.scatter(instance_size, tabu_data,
                s=np.ones(len(tabu_data)) * 40,
                c='yellow',
                marker="o",
                label='tabu',
                alpha=0.6,
                edgecolors='yellow')
    ax.legend()
    ax.grid(True)
    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")


def create_chart(heuristic_data, random_data, greedy_data, steepest_data, annealing_data, tabu_data, filename):
    # print(heuristic_data)
    fig, ax = plt.subplots()
    plt.ylabel('jakość')
    plt.xlabel('rozmiar instancji')
    plt.scatter(instance_size, heuristic_data,
                s=np.ones(len(heuristic_data)) * 20,
                c='red',
                label='heurystic',
                alpha=0.6,
                marker="*",
                edgecolors='red')
    plt.scatter(instance_size, random_data,
                s=np.ones(len(random_data)) * 20,
                c='orange',
                label='random',
                alpha=0.6,
                marker="s",
                edgecolors='orange')
    plt.scatter(instance_size, greedy_data,
                s=np.ones(len(greedy_data)) * 20,
                c='blue',
                marker="+",
                label='greedy',
                alpha=0.6,
                edgecolors='blue')
    plt.scatter(instance_size, steepest_data,
                s=np.ones(len(steepest_data)) * 20,
                c='green',
                marker="o",
                label='steepest',
                alpha=0.6,
                edgecolors='green')
    plt.scatter(instance_size, annealing_data,
                s=np.ones(len(annealing_data)) * 20,
                c='yellow',
                marker="<",
                label='annealing',
                alpha=0.6,
                edgecolors='yellow')
    plt.scatter(instance_size, tabu_data,
                s=np.ones(len(tabu_data)) * 20,
                c='black',
                marker=">",
                label='tabu',
                alpha=0.6,
                edgecolors='black')
    ax.legend()
    ax.grid(True)
    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")


def create_chart_time(heuristic_data, random_data, greedy_data, steepest_data, annealing_data, tabu_data, filename):
    # print(heuristic_data)
    fig, ax = plt.subplots()
    plt.ylabel('czas wykonania [ms]')
    plt.xlabel('rozmiar instancji')
    plt.scatter(instance_size, heuristic_data,
                s=np.ones(len(heuristic_data)) * 20,
                c='red',
                label='heurystic',
                alpha=0.6,
                marker="*",
                edgecolors='red')
    plt.scatter(instance_size, random_data,
                s=np.ones(len(random_data)) * 20,
                c='orange',
                label='random',
                alpha=0.6,
                marker="s",
                edgecolors='orange')
    plt.scatter(instance_size, greedy_data,
                s=np.ones(len(greedy_data)) * 20,
                c='blue',
                marker="+",
                label='greedy',
                alpha=0.6,
                edgecolors='blue')
    plt.scatter(instance_size, steepest_data,
                s=np.ones(len(steepest_data)) * 20,
                c='green',
                marker="o",
                label='steepest',
                alpha=0.6,
                edgecolors='green')
    plt.scatter(instance_size, annealing_data,
                s=np.ones(len(annealing_data)) * 20,
                c='yellow',
                marker="<",
                label='annealing',
                alpha=0.6,
                edgecolors='yellow')
    plt.scatter(instance_size, tabu_data,
                s=np.ones(len(tabu_data)) * 20,
                c='black',
                marker=">",
                label='tabu',
                alpha=0.6,
                edgecolors='black')
    ax.legend()
    ax.grid(True)
    #plt.yscale('log', nonposy='clip')
    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")


def create_chart_errorbar(heuristic_data, heuristic_std, random_data, random_std, greedy_data, greedy_std,
                          steepest_data, steepest_std, annealing_data, annealing_std, tabu_data, tabu_std,  filename):
    # print(heuristic_data)
    fig, ax = plt.subplots()
    plt.ylabel('jakość')
    plt.xlabel('rozmiar instancji')
    plt.errorbar(instance_size, heuristic_data, heuristic_std,
                 c='red',
                 label='heuristic',
                 alpha=0.4,
                 marker="*",
                 capsize=5)
    plt.errorbar(instance_size, random_data, random_std,
                 c='orange',
                 label='random',
                 alpha=0.4,
                 marker="s",
                 capsize=5)
    plt.errorbar(instance_size, greedy_data, greedy_std,
                 c='blue',
                 marker="+",
                 label='greedy',
                 alpha=0.4,
                 capsize=5)
    plt.errorbar(instance_size, steepest_data, steepest_std,
                 c='green',
                 marker="o",
                 label='steepest',
                 alpha=0.4,
                 capsize=5)
    plt.errorbar(instance_size, annealing_data, annealing_std,
                 c='yellow',
                 marker="<",
                 label='annealing',
                 alpha=0.4,
                 capsize=5)
    plt.errorbar(instance_size, tabu_data, tabu_std,
                 c='black',
                 marker=">",
                 label='tabu',
                 alpha=0.4,
                 capsize=5)
    ax.legend()
    ax.grid(True)
    plt.savefig(filename + ".png")
    plt.savefig(filename + ".pdf")


optimal_values = read_optimal_solutions()
heuristic, random, greedy, steepest, annealing, tabu = read_results('../AlgBattle/taiOutput_score_min.csv')
create_chart(heuristic, random, greedy, steepest, annealing, tabu, "best_quality")
heuristic, random, greedy, steepest, annealing, tabu = read_results('../AlgBattle/taiOutput_score_max.csv')
create_chart(heuristic, random, greedy, steepest, annealing, tabu, "worst_quality")
heuristic, random, greedy, steepest, annealing, tabu = read_results('../AlgBattle/taiOutput_score_median.csv')
create_chart(heuristic, random, greedy, steepest, annealing, tabu, "median_quality")
heuristic, random, greedy, steepest, annealing, tabu = read_results('../AlgBattle/taiOutput_score.csv')
heuristic_std, random_std, greedy_std, steepest_std, annealing_std, tabu_std = read_results_std(
    '../AlgBattle/taiOutput_score_std.csv')
create_chart_errorbar(heuristic, heuristic_std, random, random_std, greedy, greedy_std, steepest, steepest_std,
                      annealing, annealing_std, tabu, tabu_std, "average_quality")
heuristic, random, greedy, steepest, annealing, tabu = read_results_time('../AlgBattle/taiOutput_time.csv')
create_chart_time(heuristic, random, greedy, steepest, annealing, tabu, "time")

greedy, steepest, annealing, tabu = read_results_steps('../AlgBattle/taiOutput_steps_gs.csv')
create_chart_steps(greedy, steepest, annealing, tabu, "steps", "liczba kroków")

greedy, steepest, annealing, tabu = read_results_steps('../AlgBattle/taiOutput_checked_elems_gs.csv')
create_chart_steps(greedy, steepest, annealing, tabu, "checked_elems", "liczba sprawdzonych elementów")
