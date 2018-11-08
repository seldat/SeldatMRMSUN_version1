using DoorControllerService;
using SeldatMRMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeldatMRMS
{
    public class ProcedureControlServices:ControlService
    {
        public String ProcedureID { get; set; }
        public struct ContentDatabase {}
        public virtual event Action<ProcedureControlServices> ReleaseProcedureHandler;
        public virtual event Action<ProcedureControlServices> ErrorProcedureHandler;
        public virtual ContentDatabase RequestDataFromDataBase() { return new ContentDatabase(); }
        public ProcedureControlServices(RobotAgent robot, DoorService doorService):base(robot,doorService) {}
        protected enum ForkLiftToBuffer
        {
            FORBUF_IDLE,
            FORBUF_ROBOT_GOTO_CHECKIN_POSITION, // vị trí check in liệu có quy trình nào tại cổng
            FORBUF_ROBOT_CAME_CHECKIN_POSITION, // đã đến vị trí
            FORBUF_ROBOT_GOINTO_WAITINGZONE, // vào vùng chờ
            FORBUF_ROBOT_CAME_WAITINGZONE,  // Waiitng tại vùng chờ
            FORBUF_ROBOT_GOTO_READYZONE_GATE, // đến vùng sẵng sáng trước cổng
            FORBUF_ROBOT_CAME_READYZONE_GATE, // đến vùng trước cổng
            FORBUF_ROBOT_GATE_CHECKSENSOR,  // kiểm tra trạng thái sensors

            FORBUF_GATE_READYOPEN, // Cổng làm việc tốt / và gửi yêu cầu Mở cổng
            FORBUF_GATE_FINISHED_READYOPEN, // Hoàn Thành Mở cổng
            FORBUF_GATE_ERRORSENSOR_OPEN,

            FORBUF_GATE_READYCLOSE, // Cổng làm việc tốt / và gửi yêu cầu đóng cổng
            FORBUF_GATE_FINISHED_READYCLOSE, // Hoàn Thành đóng cổng
            FORBUF_GATE_ERRORSENSOR_CLOSE,

            FORBUF_ROBOT_DETECTLINE_PICKUP_PALLET,  // cho phép dò line và lấy pallet
            FORBUF_ROBOT_WAITING_DETECTLINE_PICKUP_PALLET, // đang trong tiến trình dò line và pick up pallet
            FORBUF_ROBOT_FINISED_DETECTLINE_PICKUP_PALLET, // hoàn thành dò line và pick up pallet

            FORBUF_ROBOT_GOBACK_READYZONE_GATE,
            FORBUF_ROBOT_CAME_GOBACK_READYZONE_GATE,

            FORBUF_ROBOT_GOTO_CHECKIN_BUFFER, // bắt đầu rời khỏi vùng GATE đi đến check in/ đảm bảo check out vùng cổng để robot kế tiếp vào làm việc
            FORBUF_ROBOT_FINISHED_GOTO_CHECKIN_BUFFER, // hoàn thành đến vùng check in/ kiểm tra có robot đang làm việc vùng này và lấy vị trí line và pallet

            FORBUF_ROBOT_GOTO_FRONTLINE_BUFFER, // ROBOT cho tiến vào vị trí đầu line // vẩn dò đường bằng laser
            FORBUF_ROBOT_FINISED_GOTO_FRONTLINE_BUFFER, // hoàn thành đến vùng check in/ kiểm tra có robot đang làm việc vùng này và lấy vị trí line và pallet
            FORBUF_ROBOT_START_DETECTLINE_TO_LINE_INSIDE_BUFFER, // bắt đầu dò line để đến vị trí line trong buffer
            FORBUF_ROBOT_WAIITNG_DETECTLINE_TO_LINE_INSIDE_BUFFER, // đang đợi dò line để đến vị trí line trong buffer
            FORBUF_ROBOT_CAME_LINE_INSIDE_BUFFER, // đến vị trí line trong buffer

            FORBUF_ROBOT_START_DETECTLINE_DROPDOWN_PALLET_INSIDE_BUFFER, // bắt đầu dò line và vị trí pallet trong buffer
            FORBUF_ROBOT_WAITING_DETECTLINE_DROPDOWN_PALLET_INSIDE_BUFFER, // bắt đầu dò line và vị trí pallet trong buffer
            FORBUF_ROBOT_FINISED_DETECTLINE_DROPDOWN_PALLET_INSIDE_BUFFER, // bắt đầu dò line và vị trí pallet trong buffer // lưu vào cơ sở dữ liệu

            FORBUF_ROBOT_GOBACK_FRONTLINE_BUFFER, // bắt đầu go back vi tri đau line buffer
            FORBUF_ROBOT_WAIITING_GOBACK_FRONTLINE_BUFFER, // đợi
            FORBUF_ROBOT_CAME_GOBACK_FRONTLINE_BUFFER, // đã đến vị trí đầu line
            FORBUF_ROBOT_RELEASED, // trả robot về robotmanagement để nhận quy trình mới
        }
        protected enum BufferToMachine
        {
            BUFMAC_IDLE,
            BUFMAC_ROBOT_GOTO_CHECKIN_BUFFER, // bắt đầu rời khỏi vùng GATE đi đến check in/ đảm bảo check out vùng cổng để robot kế tiếp vào làm việc
            BUFMAC_ROBOT_FINISHED_GOTO_CHECKIN_BUFFER, // hoàn thành đến vùng check in/ kiểm tra có robot đang làm việc vùng này và lấy vị trí line và pallet

            BUFMAC_ROBOT_GOTO_FRONTLINE_BUFFER, // ROBOT cho tiến vào vị trí đầu line // vẩn dò đường bằng laser
            BUFMAC_ROBOT_FINISED_GOTO_FRONTLINE_BUFFER, // hoàn thành đến vùng check in/ kiểm tra có robot đang làm việc vùng này và lấy vị trí line và pallet
            BUFMAC_ROBOT_START_DETECTLINE_TO_LINE_INSIDE_BUFFER, // bắt đầu dò line để đến vị trí line trong buffer
            BUFMAC_ROBOT_WAIITNG_DETECTLINE_TO_LINE_INSIDE_BUFFER, // đang đợi dò line để đến vị trí line trong buffer
            BUFMAC_ROBOT_CAME_LINE_INSIDE_BUFFER, // đến vị trí line trong buffer

            BUFMAC_ROBOT_START_DETECTLINE_PICKUP_PALLET_INSIDE_BUFFER, // bắt đầu dò line và vị trí pallet trong buffer
            BUFMAC_ROBOT_WAITING_DETECTLINE_PICKUP_PALLET_INSIDE_BUFFER, // bắt đầu dò line và vị trí pallet trong buffer
            BUFMAC_ROBOT_FINISED_DETECTLINE_PICKUP_PALLET_INSIDE_BUFFER, // bắt đầu dò line và vị trí pallet trong buffer // lưu vào cơ sở dữ liệu

            BUFMAC_ROBOT_GOBACK_FRONTLINE_BUFFER, // bắt đầu go back vi tri đau line buffer
            BUFMAC_ROBOT_WAIITING_GOBACK_FRONTLINE_BUFFER, // đợi
            BUFMAC_ROBOT_CAME_GOBACK_FRONTLINE_BUFFER, // đã đến vị trí đầu line
           

            BUFMAC_ROBOT_GOTO_CHECKIN_MACHINE, // vị trí check in liệu có quy trình nào tại MACHINE
            BUFMAC_ROBOT_CAME_CHECKIN_MACHINE, // đã đến vị trí
            BUFMAC_ROBOT_ASK_GOINSIDE_PALLETZONE_AT_MACHINE, // robot yêu cầu được phép vào vùng thả pallet
            BUFMAC_ROBOT_WAIITNG_ACCEPT_GOINSIDE_PALLETZONE_AT_MACHINE, // robot yêu cầu được phép vào vùng thả pallet
            BUFMAC_ROBOT_ACCEPTED_GOINSIDE_PALLETZONE_AT_MACHINE, // robot được đồng ý vào vùng thả pallet


            BUFMAC_ROBOT_DETECTLINE_DROPDOWN_PALLET,  // cho phép dò line vàthả pallet
            BUFMAC_ROBOT_WAITING_DETECTLINE_DROPDOWN_PALLET, // đang trong tiến trình dò line và thả pallet
            BUFMAC_ROBOT_FINISED_DETECTLINE_DROPDOWN_PALLET, // hoàn thành dò line và thả pallet

            BUFMAC_ROBOT_GOBACK_FRONTLINE , // quay lại vị trí đầu line
            BUFMAC_ROBOT_CAME_GOBACK_FRONTLINE,
            BUFMAC_ROBOT_RELEASED, // trả robot về robotmanagement để nhận quy trình mới
        }


    }
}
