from myTools import *
import threading
import letsgoFarm as lf


def run_letsgoFarm(btn_a, btn_d):
    """开始挂机"""
    btn_active(btn_a)
    btn_disable(btn_d)
    exit_event.clear()
    thread = threading.Thread(target=lambda: lf.start())
    thread.daemon = True
    thread.start()


def threading_fun(fun):
    print_log("请在3秒内点击游戏画面", "green")
    exit_event.clear()
    thread = threading.Thread(target=lambda: fun())
    create_progressbar()
    for _ in range(6):
        exit_event.wait(0.5)
        update_progressbar(50)
    thread.daemon = True
    thread.start()


# 创建一个窗口并设置窗口的标题
window = create_window("星宝农场自动化")

# 创建一个列表框，第一行
create_listbox()

# 创建一个进度条，第二行
create_progressbar()

# 创建三个个按钮，启动、停止、退出，第四行
btn_run = create_button("运行", 2, 0)
btn_run.config(command=lambda: run_letsgoFarm([btn_stop], [btn_run]))
btn_stop = create_button("停止", 2, 1)
btn_stop.config(command=lambda: stop_letsgoFarm([btn_run], [btn_stop]))

btn_quit = create_button("退出", 2, 2)
btn_quit.config(command=window.quit)


def focus_out(event):
    var = loop.get()
    if len(var) > 3:
        loop.delete(0, "end")
    try:
        lgf_config["loops"] = int(var)
    except:
        print_log("请输入数字", "red")


# 创建三个按钮，前往农场，前往牧场，前往鱼塘，第三行
loop = tk.Entry(window, width=3)
loop.insert(0, "538")
loop.grid(row=3, column=0)
loop.bind("<FocusOut>", focus_out)
text = tk.Label(window, text="秒执行一次")
text.grid(row=3, column=1)

# 置顶窗口
window.attributes("-topmost", True)
# 设置窗口大小不可改变
window.resizable(False, False)
# 启动事件循环
window.mainloop()
