

namespace OrderSystem
{
    public class OrderSystemEvent 
    {
        /// <summary>
        /// 启动 
        /// </summary>
        public const string STARTUP = "StartUp";
        /// <summary>
        /// 点菜
        /// </summary>
        public const string ORDER = "Order";
        /// <summary>
        /// 取消点餐
        /// </summary>
        public const string CANCEL_ORDER = "CancelOrder";
        /// <summary>
        /// 呼叫服务员
        /// </summary> 
        public const string CALL_WAITER = "CallWaiter";
        /// <summary>
        /// 结账
        /// </summary>
        public const string PAY = "Pay";
        /// <summary>
        /// 服务员接收付款
        /// </summary>
        public const string GET_PAY = "GetPay";
        /// <summary>
        /// 通知厨师
        /// </summary>
        public const string CALL_COOK = "CallCook";
        /// <summary>
        /// 上菜
        /// </summary>
        public const string SERVER_FOOD = "ServerFood";
        /// <summary>
        /// 上菜单
        /// </summary>
        public const string UPMENU = "UpMenu";
        /// <summary>
        /// 提交菜单
        /// </summary>
        public const string SUBMITMENU = "SubmitMenu";
        /// <summary>
        /// 服务员上菜
        /// </summary>
        public const string FOOD_TO_CLIENT = "FoodToClient";

        /// <summary>
        /// 客人走了
        /// </summary>
        public const string GUEST_BE_AWAY = "guest_be_away";

        /// <summary>
        /// 来客人了刷新
        /// </summary>
        public const string ADD_GUEST = "add_guest";

        /// <summary>
        /// 获取菜单
        /// </summary>
        public const string GET_ORDER = "get_order";
        /// <summary>
        /// 刷新服务员的信息
        /// </summary>
        public const string ResfrshWarite = "resfrshWarite";
        /// <summary>
        /// 刷新厨师
        /// </summary>
        public const string ResfrshCook = "resfrshcook";
        /// <summary>
        /// 厨师做菜
        /// </summary>
        public const string CookCooking = "cookCooking";
        /// <summary>
        /// 选择空闲的服务员 上菜
        /// </summary>
        public const string selectWaiter = "selectWaiter";
        /// <summary>
        /// 去更改总数据的信息（房间订单和点菜订单的汇总）
        /// </summary>
        public const string Add_AllInfo = "add_allinfo";
        /// <summary>
        /// 发送一条获取房间列表的消息
        /// </summary>
        public const string Get_RoomInfo = "get_roominfo";
        /// <summary>
        /// 获取房间表将房间数据添加到代理中
        /// </summary>
        public const string GET_ROOMORDER = "get_roomorder";
        /// <summary>
        /// 弹出房间订单
        /// </summary>
        public const string UP_ROOMMENU = "up_roommenu";
        /// <summary>
        /// 选择完房间信息点击入住
        /// </summary>
        public const string ROOMSUBMITMENU = "roomsubmitmenu";
        /// <summary>
        /// 有人入住刷新房间显示
        /// </summary>
        public const string CHECKIN  = "checkin";
        /// <summary>
        /// 刷新后台显示
        /// </summary>
        public const string RefreshBack = "refreshback";
        /// <summary>
        /// 刷新金钱显示
        /// </summary>
        public const string RefreshMoney = "refreshmoney";
    }
}