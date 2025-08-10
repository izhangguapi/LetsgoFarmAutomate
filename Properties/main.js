const btnOffset = {
  blank: { x: 0.1, y: 0.999 }, // 空白位置
  jumpBtn: { x: 0.741, y: 0.822 }, // 跳跃按钮
  resetBtn: { x: 0.959, y: 0.907 }, // 重置按钮
  leftBtn: { x: 0.76, y: 0.65 }, // 无人机、放大镜发射
  rightBtn: { x: 0.86, y: 0.55 }, // 收获水族箱、神农许愿
  closeBtn: { x: 0.72, y: 0.27 }, // 窗口关闭按钮
  // refuseBtn: { x: 0.36, y: 0.65 }, // 窗口左侧按钮
  skipBtn: { x: 0.625, y: 0.733 }, // 鱼塘跳过按钮
  acceptBtn: { x: 0.63, y: 0.65 }, // 窗口右侧的按钮
  okBtn: { x: 0.5, y: 0.65 }, // 窗口中间确定按钮
  bigBtn: { x: 0.875, y: 0.8 }, // 右下角大按钮
  // 收获鱼缸，滑动镜头的偏移量
  aquarium: {
    left: 0.05,
    top: 0.13,
  },
  // 泡温泉，
  hotspring: {
    top: 0.178, // 向上滑动镜头的偏移量
    right: 0.098, // 向左滑动镜头的偏移量
    socializing: { x: 0.9625, y: 0.3 }, // 社交
    inputbox: { x: 0.7, y: 0.94 }, // 输入框
    oneself: { x: 0.77, y: 0.16 }, // 社交中自己这块空白区域
    visit: { x: 0.965, y: 0.3 }, // 拜访按钮
    gohome: { x: 0.91, y: 0.07 }, // 回家
  },
  // 许愿
  pray: {
    btn: { x: 0.785, y: 0.88 }, // 许愿按钮
    left: { x: 0.72, y: 0.6 }, // 左侧金币
    right: { x: 0.875, y: 0.6 }, //右侧经验
  },
};

/**
 * 休眠函数 - 使用Promise封装setTimeout
 * @param {number} ms 等待的毫秒数
 * @returns {Promise<void>} 可被await的Promise
 */
const sleep = (ms) => new Promise((resolve) => setTimeout(resolve, ms));

/**
 * 键盘模拟模块 - 所有键盘模拟操作的集合
 */
const Keyboard = {
  /**
   * 创建键盘事件
   * @param {string} type 事件类型
   * @param {string} key 按键字符
   * @returns {KeyboardEvent}
   */
  createKeyboardEvent(type, key) {
    return new KeyboardEvent(type, {
      bubbles: true,
      cancelable: true,
      key,
      code: `Key${key.toUpperCase()}`,
      keyCode: key.charCodeAt(0),
    });
  },

  /**
   * 模拟键盘按下
   * @param {string} key 按键字符
   */
  async down(key, wait = 0) {
    document.dispatchEvent(this.createKeyboardEvent("keydown", key));
    await sleep(wait);
  },

  /**
   * 模拟键盘释放
   * @param {string} key 按键字符
   */
  async up(key, wait = 0) {
    document.dispatchEvent(this.createKeyboardEvent("keyup", key));
    await sleep(wait);
  },

  /**
   * 模拟完整按键（按下并释放）
   * @param {string} key 按键字符
   * @param {number} wait 按键按下后等待时间
   * @param {number} duration 按住时长(毫秒)
   * @returns {Promise<void>}
   */
  async press(key, wait = 0, duration = 100) {
    await this.down(key); // 先按下按键
    await sleep(duration); // 等待按住时长
    await this.up(key); // 释放按键
    await sleep(wait); // 最后等待
  },
};

const GameState = {
  // 页面宽高
  clientWidth: 0,
  clientHeight: 0,
  // 实际游戏宽高度
  gameWidth: 0,
  gameHeight: 0,
  // 偏移量计算
  offsetLeft: 0,
  offsetTop: 0,
};

/**
 * 鼠标模拟模块 - 处理所有鼠标操作
 */
const Mouse = {
  /**
   * 获取游戏视图元素
   * @returns {HTMLElement|null}
   */
  getGameView() {
    const gameView = document.querySelector(".gmsdk-video-player");
    if (!gameView) {
      console.error("游戏视图元素未找到");
      return false;
    }
    // console.log("现在的", gameView.clientWidth, gameView.clientHeight);

    if (
      GameState.clientWidth !== gameView.clientWidth ||
      GameState.clientHeight !== gameView.clientHeight
    ) {
      GameState.clientWidth = gameView.clientWidth;
      GameState.clientHeight = gameView.clientHeight;
      // 计算宽高度
      const calculateWidth = gameView.clientHeight * (16 / 9);
      const calculateHeight = gameView.clientWidth * (9 / 16);
      if (calculateWidth > gameView.clientWidth) {
        GameState.gameWidth = gameView.clientWidth;
        GameState.gameHeight = calculateHeight;
        GameState.offsetTop = (gameView.clientHeight - calculateHeight) / 2;
        GameState.offsetLeft = 0;
      } else {
        GameState.gameWidth = calculateWidth;
        GameState.gameHeight = gameView.clientHeight;
        GameState.offsetLeft = (gameView.clientWidth - calculateWidth) / 2;
        GameState.offsetTop = 0;
      }
    }
    return true;
  },

  /**
   * 将相对坐标转换为游戏实际坐标
   * @param {{x: number, y: number}} ratioCoords 相对坐标比例
   * @returns {{x: number, y: number}}
   */
  convertToGameCoords(ratioCoords) {
    this.getGameView();
    if (!ratioCoords) {
      console.error("坐标为空", ratioCoords);
      return null;
    }
    if (
      ratioCoords.x < 0 ||
      ratioCoords.x > 1 ||
      ratioCoords.y < 0 ||
      ratioCoords.y > 1
    ) {
      console.error("坐标超出范围", ratioCoords);
      return null;
    }
    const x = GameState.offsetLeft + GameState.gameWidth * ratioCoords.x;
    const y = GameState.offsetTop + GameState.gameHeight * ratioCoords.y;
    return { x, y };
  },

  /**
   * 模拟点击操作
   * @param {number[]} ratioCoords 相对坐标比例 [x比例, y比例]
   * @param {number} duration 点击耗时(毫秒)
   * @param {number} wait 最后等待时间(毫秒)
   * @returns {Promise<boolean>} 操作是否成功
   */
  async click(ratioCoords, wait = 100, duration = 100) {
    // 计算实际坐标
    const coords = this.convertToGameCoords(ratioCoords);
    if (!coords) {
      return false;
    }
    // 获取目标元素
    const targetElement = document.elementFromPoint(coords.x, coords.y);
    if (!targetElement) {
      console.warn("点击位置无可交互元素", coords.x, coords.y);
      return false;
    }
    // else {
    //   console.log("点击位置: ", coords.x, coords.y);
    // }
    // 鼠标移动
    this.dispatchEvent(targetElement, "mousemove", coords.x, coords.y);
    await sleep(duration);
    // 鼠标按下
    this.dispatchEvent(targetElement, "mousedown", coords.x, coords.y);
    await sleep(duration);
    // 鼠标释放
    this.dispatchEvent(targetElement, "mouseup", coords.x, coords.y);
    await sleep(wait);
    return true;
  },

  /**
   * 模拟拖动操作
   * @param {string} direction 方向 (left/right/top/bottom)
   * @param {number} distanceRatio 距离比例 (0~1)
   * @param {number} duration 滑动耗时(毫秒)
   * @returns {Promise<boolean>} 操作是否成功
   */
  async drag(direction, distanceRatio, duration = 500) {
    this.getGameView();
    // 方向映射
    const directionMap = {
      left: [-1, 0],
      right: [1, 0],
      top: [0, -1],
      bottom: [0, 1],
    };
    // 验证方向
    if (!directionMap[direction]) {
      console.error(`无效方向: ${direction}，支持: left/right/top/bottom`);
      return false;
    }
    // 获取游戏元素
    this.getGameView();
    // 计算中心点坐标
    const centerX = GameState.clientWidth / 2;
    const centerY = GameState.clientHeight / 2;
    // 计算移动距离
    const isHorizontal = direction === "left" || direction === "right";
    const distance = isHorizontal
      ? GameState.gameWidth * distanceRatio
      : GameState.gameHeight * distanceRatio;
    // 计算终点坐标
    const [xMod, yMod] = directionMap[direction];
    const endX = centerX + distance * xMod;
    const endY = centerY + distance * yMod;
    // 获取目标元素
    const targetElement = document.elementFromPoint(centerX, centerY);
    if (!targetElement) {
      console.warn("拖动起点无可交互元素");
      return false;
    }
    // 开始拖动
    this.dispatchEvent(targetElement, "mousemove", centerX, centerY);
    this.dispatchEvent(targetElement, "mousedown", centerX, centerY);
    // 平滑移动
    const steps = duration / 10;
    const stepX = (endX - centerX) / steps;
    const stepY = (endY - centerY) / steps;

    for (let i = 0; i <= steps; i++) {
      await sleep(10);
      const currentX = centerX + stepX * i;
      const currentY = centerY + stepY * i;
      this.dispatchEvent(targetElement, "mousemove", currentX, currentY);
    }

    // 结束拖动
    this.dispatchEvent(targetElement, "mouseup", endX, endY);
    await sleep(500);

    return true;
  },

  /**
   * 创建并触发鼠标事件
   * @param {HTMLElement} target 目标元素
   * @param {string} type 事件类型：mousemove, mousedown, mouseup
   * @param {number} x 客户端X坐标
   * @param {number} y 客户端Y坐标
   */
  dispatchEvent(target, type, x, y) {
    if (!target) return false;

    const event = new MouseEvent(type, {
      clientX: x,
      clientY: y,
      bubbles: true,
    });
    return target.dispatchEvent(event);
  },
};

/**
 * 在游戏输入框输入字符串
 * @param {string} text 要输入的文本
 */
async function fakeInput(text, wait = 1000) {
  const input = document.activeElement;

  // 模拟真实输入流
  input.focus();
  input.value = text;

  const event = new Event("input", {
    bubbles: true,
    cancelable: false,
    composed: true,
  });

  input.dispatchEvent(event);
  await sleep(wait);
  // 触发游戏引擎的验证
  // setTimeout(() => {
  //   const changeEvent = new Event("change", {
  //     bubbles: true,
  //   });
  //   input.dispatchEvent(changeEvent);
  // }, 1000);
}

const actionHandlers = {
  /**
   * 初始化
   * @param {number} time 耗时（多少毫秒后执行）
   */
  init: async () => {
    // 音量设为0，修改网络延迟和操作按钮的位置
    localStorage.setItem("gameVoice", 0);
    localStorage.setItem("xianyou-drag-delay", '{"x":10,"y":10}');
    localStorage.setItem("xianyou-pc-drag-menu", '{"x":10,"y":1}');
    // 关闭增强，画质调最低
    // 检查并应用画质设置
    if (
      localStorage.getItem("gameFsr") !== "false" ||
      localStorage.getItem("gameSharpness") !== "1"
    ) {
      localStorage.setItem("gameFsr", false);
      localStorage.setItem("gameSharpness", 1);
      return location.reload(); // 重新加载页面并终止后续代码执行
    }
    // 移除干扰元素（左上角的创建快捷方式、顶部快捷键提示、跟随鼠标的提示）
    [".dw", ".g-pc-k", ".g-pc-m-tip"].forEach((selector) =>
      document.querySelector(selector)?.remove()
    );
    // 关闭辅助屏
    document.querySelector(".g-pc-s-v-close")?.click();
    // console.log("初始化完成");
  },
  /**
   * 关闭屏幕上的弹窗
   */
  clearScreen: async () => {
    // 跳跃一下，解除省电模式
    // await Mouse.click(btnOffset.jumpBtn);

    // 网络波动重新连接
    for (let index = 0; index < 3; index++) {
      await Mouse.click(btnOffset.acceptBtn, 3000);
      await Mouse.click(btnOffset.okBtn);
    }
    // 释放wasd键
    for (const element of ["W", "A", "S", "D"]) {
      // 拒绝好友拉倒身边
      await Mouse.click(btnOffset.closeBtn);
      // 点击空白地方关闭鱼卡
      await Mouse.click(btnOffset.blank);
      // 点击钓鱼的跳过按钮
      await Mouse.click(btnOffset.skipBtn);
      await Keyboard.press(element);
    }
    // 重置位置
    await Mouse.click(btnOffset.resetBtn);
  },

  /**
   * 寻找无人机
   */
  findDrone: async () => {
    await Keyboard.down("A", 300);
    await Mouse.click(btnOffset.jumpBtn, 700);
    Keyboard.up("A");
  },
  /**
   * 运行一次无人机
   */
  runDrone: async () => {
    await actionHandlers.clearScreen();
    // 走到无人机
    await actionHandlers.findDrone();
    // 执行无人机
    await Mouse.click(btnOffset.leftBtn);
  },
  /**
   * 寻找发射器
   * @param {boolean} selectLeft 是否点击左边按钮（默认true）
   * 如果为true，则点击左边放大镜；否则点击农务车
   */
  findTransmitter: async (selectLeft = true) => {
    Keyboard.down("D");
    await Mouse.click(btnOffset.jumpBtn, 500);
    await Keyboard.up("D", 1000);

    if (selectLeft) {
      // 点击放大镜
      await Mouse.click(btnOffset.leftBtn, 1000);
    } else {
      // 点击农务车
      await Mouse.click(btnOffset.rightBtn, 1000);
    }
  },
  /**
   * 自动收水族箱
   */
  aquarium: async () => {
    await actionHandlers.clearScreen();
    await actionHandlers.findTransmitter();
    // 移动视角
    await Mouse.drag("top", btnOffset.aquarium.top);
    await Mouse.drag("left", btnOffset.aquarium.left);
    await Mouse.click(btnOffset.bigBtn, 3200);
    await Mouse.click(btnOffset.rightBtn, 3000);
    await Mouse.click(btnOffset.blank);
    console.log("水族箱收获完成");
    callWinForms("水族箱收获完成", "green");
  },
  /**
   * 自动许愿，耗时14000毫秒
   * @param {boolean} select
   */
  pray: async (select) => {
    await actionHandlers.clearScreen();
    Keyboard.down("S");
    await Mouse.click(btnOffset.jumpBtn, 1800);
    Keyboard.up("S");
    await Mouse.click(btnOffset.rightBtn, 500);
    if (select) {
      await Mouse.click(btnOffset.pray.left);
    } else {
      await Mouse.click(btnOffset.pray.right);
    }
    await Mouse.click(btnOffset.pray.btn, 8000);
    await Mouse.click(btnOffset.blank);
    console.log("许愿完成");
    callWinForms("许愿完成", "green");
  },
  /**
   * 自动泡温泉，自家喝茶耗时29150毫秒，好友耗时57900
   * @param {boolean} tea 是否喝茶
   * @param {string} uid 好友的uid
   */
  hotspring: async (tea = true, uid = null) => {
    await actionHandlers.clearScreen();
    if (uid) {
      await Mouse.click(btnOffset.hotspring.socializing, 2000);
      await Mouse.click(btnOffset.hotspring.inputbox, 2000);
      await fakeInput(uid);
      await Mouse.click(btnOffset.hotspring.oneself, 3000);
      await Mouse.click(btnOffset.hotspring.visit, 10000);
    }
    await Mouse.click(btnOffset.resetBtn);
    await actionHandlers.findTransmitter();
    // 移动视角
    await Mouse.drag("top", btnOffset.hotspring.top);
    await Mouse.drag("right", btnOffset.hotspring.right);
    await Mouse.click(btnOffset.bigBtn, 4000);
    await Mouse.click(btnOffset.bigBtn, 15000);
    if (tea) {
      await Mouse.click(btnOffset.leftBtn, 1000);
      await Mouse.click(btnOffset.acceptBtn, 1000);
    }
    if (uid) {
      await Mouse.click(btnOffset.hotspring.gohome, 10000);
    } else {
      await Mouse.click(btnOffset.resetBtn);
    }
    console.log("泡温泉完成");
    callWinForms("泡温泉完成", "green");
  },
};
actionHandlers.init();

// ========== 队列系统 ==========
const queue = ((tasks) => ({
  add: (t) => (tasks.push(t), !queue.p && queue.process()),
  p: false,
  process: async () => {
    queue.p = true;
    while (tasks.length) await tasks.shift()().catch(console.error);
    queue.p = false;
  },
}))([]);
// ================================

// 事件监听器
window.addEventListener("message", ({ data: { action, params } }) => {
  if (!action) return;
  const handler = actionHandlers[action];
  if (!handler) return console.error("未找到操作:", action);
  queue.add(() => handler(...(params || [])));
});

// 发送消息
// window.postMessage({ action: 'runDrone' }, '*');
// 带参数
// window.postMessage({ action: 'hotspring' ,params: [true, "725149317"]}, '*');
