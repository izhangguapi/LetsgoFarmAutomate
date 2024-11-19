import selectWindow, threading, win32gui, datetime
import tkinter as tk
from tkinter import ttk
import myTools as mt
import letsgoFarm as lgf
import re


def open_select_window():
    """打开选择窗口"""
    selectWindow.create_window()
    window.wait_window(selectWindow.window)
    # if mt.title != None:
    #     window.title(mt.title + " - " + str(mt.hwnd))
    # print("窗口关闭")
    try:
        title = mt.title + " - " + str(mt.hwnd)
        window.title(title)
        game_rect = win32gui.GetWindowRect(mt.hwnd)
        this_window = win32gui.FindWindowEx(0, 0, None, title)
        x = game_rect[0]
        y = game_rect[1] + 585
        win32gui.MoveWindow(this_window, x, y, 0, 0, True)
        mt.log_name = (
            str(datetime.datetime.now().strftime("%Y年%m月%d日_%H时%M分%S秒"))
            + mt.title
            + "_"
            + str(mt.hwnd)
            + ".txt"
        )
    except:
        pass


def run_letsgoFarm():
    """开始挂机"""
    run.config(state=tk.DISABLED)
    select.config(state=tk.DISABLED)
    mt.exit_event.clear()
    thread = threading.Thread(target=lgf.start)
    thread.daemon = True
    thread.start()
    stop.config(state=tk.NORMAL)


def stop_letsgoFarm():
    """停止挂机"""
    run.config(state=tk.NORMAL)
    select.config(state=tk.NORMAL)
    mt.exit_event.set()
    stop.config(state=tk.DISABLED)


# def on_click(event):
#     window.focus_get()


# 创建标签框架（农牧鱼）
class CreateLF:
    enable = None
    timer = None
    h = "00"
    m = "00"
    s = "00"

    def __init__(self, window):
        self.enable = tk.BooleanVar(value=True)
        self.timer = tk.BooleanVar()

    def on_mouse_leave(self, event):
        window.focus_set()

    def focus_out(self, event):
        num = event.widget.get().strip()
        name = event.widget.winfo_name()

        if len(num) > 2 or not num.isdigit():
            event.widget.delete(0, tk.END)
            return
        if len(num) == 1:
            num = "0" + num
        if name == "h":
            if int(num) > 23:
                event.widget.delete(0, tk.END)
                num = "00"
            self.h = num
        elif name == "m":
            if int(num) > 59:
                event.widget.delete(0, tk.END)
                num = "00"
            self.m = num
        elif name == "s":
            if int(num) > 59:
                event.widget.delete(0, tk.END)
                num = "00"
            self.s = num

        # 获取LabelFrame名称
        lf_name = str(event.widget.master).split(".")[-1]
        today = str(datetime.date.today())
        time = self.h + ":" + self.m + ":" + self.s
        if lf_name == "农场":
            mt.lgf_config["farm_time"] = today + " " + time
        elif lf_name == "牧场":
            mt.lgf_config["pasture_time"] = today + " " + time
        elif lf_name == "鱼塘":
            mt.lgf_config["fishpond_time"] = today + " " + time
        else:
            mt.lgf_config["prayers_time"] = today + " " + time

    def focus_out_text(self, event):
        text = event.widget
        content = text.get("1.0", "end-1c")  # 获取Text组件的内容
        lines = content.split("\n")  # 将内容按行分割
        # print(lines)
        if len(lines) > 3:
            text.delete("3.10", "end-1c")

        for i in range(1, len(lines)):
            text.delete(str(i) + ".10", str(i) + ".end")
        list_uid = text.get("1.0", "end-1c").split("\n")
        mt.lgf_config["prayers_uid"] = list_uid

    def create(
        self,
        title,
        col,
        is_enable=True,
        enable_text="启用",
        is_timer=True,
        timer_text="定时关闭",
        is_text=False,
        rowspan=1,
    ):
        self.lf = tk.LabelFrame(window, text=title, name=title)
        self.lf.grid(row=0, column=col, rowspan=rowspan, sticky="nsew")

        enable = tk.Checkbutton(
            self.lf,
            text=enable_text,
            variable=self.enable,
        )
        enable.grid(row=0, column=0, columnspan=5)
        enable.bind("<Leave>", self.get_checkbtn)
        enable.config(state=tk.DISABLED)


def create_fram_lf():
    """创建农场标签框架"""
    lf = tk.LabelFrame(window, text="农场", name="农场")
    lf.grid(row=0, column=0, padx=(10, 0), pady=5, sticky="nsew")
    mt.lgf_config["farm_enable"] = tk.BooleanVar(value=True)
    enable = tk.Checkbutton(lf, text="启用", variable=mt.lgf_config["farm_enable"])
    enable.grid(row=0, column=0, columnspan=2)
    # enable.bind("<Leave>", get_checkbtn)

    lable = tk.Label(lf, text="地块数量")
    lable.grid(row=1, column=0)
    text = tk.Entry(lf, width=2)
    text.insert(0, "30")
    text.grid(row=1, column=1, padx=(0, 5))
    # text.bind("<FocusOut>", focus_out)


def create_pasture_lf():
    """创建牧场标签框架"""
    lf = tk.LabelFrame(window, text="牧场", name="牧场")
    lf.grid(row=0, column=1, padx=(5, 0), pady=5, sticky="nsew")
    mt.lgf_config["pasture_enable"] = tk.BooleanVar(value=True)
    enable = tk.Checkbutton(lf, text="启用", variable=mt.lgf_config["pasture_enable"])
    enable.grid(row=0, column=0, columnspan=2)
    # enable.bind("<Leave>", get_checkbtn)

    lable = tk.Label(lf, text="地块数量")
    lable.grid(row=1, column=0)
    text = tk.Entry(lf, width=2)
    text.insert(0, "15")
    text.grid(row=1, column=1, padx=(0, 5))
    # text.bind("<FocusOut>", focus_out)


def create_fishpond_lf():
    """创建鱼塘标签框架"""
    lf = tk.LabelFrame(window, text="鱼塘", name="鱼塘")
    lf.grid(row=0, column=2, padx=(5, 0), pady=5, sticky="nsew")
    mt.lgf_config["fishpond_enable"] = tk.BooleanVar(value=True)
    enable = tk.Checkbutton(lf, text="启用", variable=mt.lgf_config["fishpond_enable"])
    enable.grid(row=0, column=2)
    # enable.bind("<Leave>", get_checkbtn)


def create_processors_lf():
    """创建加工器标签框架"""
    lf = tk.LabelFrame(window, text="加工器", name="加工器")
    lf.grid(row=0, column=3, padx=(5, 0), pady=5, sticky="nsew")
    mt.lgf_config["processors_enable"] = tk.BooleanVar(value=True)
    enable = tk.Checkbutton(
        lf, text="启用", variable=mt.lgf_config["processors_enable"]
    )
    enable.grid(row=0, column=2)
    # enable.bind("<Leave>", get_checkbtn)


def create_settings_lf():
    """创建设置标签框架"""

    def focus_out(event):
        var = loop.get()
        if len(var) > 3:
            loop.delete(0, "end")
        try:
            mt.lgf_config["loops"] = int(var)
        except:
            mt.print_log("请输入数字", "red")

    lf = tk.LabelFrame(window, text="设置", name="设置")
    lf.grid(row=0, column=4, padx=(5, 0), pady=5, sticky="nsew")

    mt.lgf_config["vip"] = tk.BooleanVar(value=True)
    vip = tk.Checkbutton(lf, text="是否开通月卡", variable=mt.lgf_config["vip"])
    vip.grid(row=0, column=0, columnspan=2)

    loop = tk.Entry(lf, width=3)
    loop.insert(0, "538")
    loop.grid(row=1, column=0)
    loop.bind("<FocusOut>", focus_out)
    text = tk.Label(lf, text="秒执行一次")
    text.grid(row=1, column=1)


def create_other_lf():
    """创建控制标签框架"""
    global select, run, stop
    lf = tk.LabelFrame(window, text="控制")
    lf.grid(row=0, column=5, padx=5, pady=5, sticky="nsew")

    select = tk.Button(lf, text="选择窗口")
    select.config(command=open_select_window)
    select.grid(row=0, column=0, padx=(5, 0), pady=5)

    run = tk.Button(lf, text="开始运行")
    run.config(command=run_letsgoFarm)
    run.grid(row=0, column=1, padx=(5, 0), pady=5)
    # 测试按钮
    # test = tk.Button(lf, text="测试按钮")
    # test.config(command=lgf.ocr_fish)
    # test.grid(row=1, column=0)
    stop = tk.Button(lf, text="停止程序")
    stop.config(command=stop_letsgoFarm, state=tk.DISABLED)
    stop.grid(row=0, column=2, padx=5, pady=5)

    quit = tk.Button(lf, text="退出程序")
    quit.grid(row=1, column=0, columnspan=3, padx=5, pady=(0, 5), sticky="nsew")
    quit.config(command=window.quit)


if __name__ == "__main__":
    window = tk.Tk()
    window.title("请选择游戏窗口")

    create_fram_lf()
    create_pasture_lf()
    create_fishpond_lf()
    create_processors_lf()
    create_settings_lf()
    create_other_lf()

    # 创建日志列表
    mt.create_listbox(window)

    window.resizable(False, False)
    window.mainloop()
