using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
/*L1=10
L2=5
H=6
theta=-%pi/2;
X1=sqrt(L1^2+(H/2)^2)*cos(theta+atan(H/2,L1))
Y1=sqrt(L1^2+(H/2)^2)*sin(theta+atan(H/2,L1))

X2=sqrt(L1^2+(H/2)^2)*cos(theta+atan(-H/2,L1))
Y2=sqrt(L1^2+(H/2)^2)*sin(theta+atan(-H/2,L1))


X3=sqrt(L2^2+(H/2)^2)*cos(theta+atan(-H/2,-L2))
Y3=sqrt(L2^2+(H/2)^2)*sin(theta+atan(-H/2,-L2))

X4=sqrt(L2^2+(H/2)^2)*cos(theta+atan(H/2,-L2))
Y4=sqrt(L2^2+(H/2)^2)*sin(theta+atan(H/2,-L2))

X=[ X1 X2 X3 X4]
Y=[ Y1 Y2 Y3 Y4]
plot([0 X1],[0 Y1],'g.-')
plot([0 X2],[0 Y2],'g-.')
plot([0 X3],[0 Y3],'.g-')*/


namespace SeldatMRMS.Management.RobotManagent
{
    class RobotAgentService
    {
        public double RealWidth { get; set; }
        public double RealHeigth { get; set; }
        public double currentX;
        public double currentY;
        public double Angle;
        public double L1;
        public double L2;
        public double H;
        RobotAgentService()
        {

        }
        public virtual Point TopHeader()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(H / 2, L1));
            double y = currentY+ Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(H / 2, L1));
            return new Point(x, y);
        }
        public virtual Point BottomHeader()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(-H / 2, L1));
            double y = currentY + Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(-H / 2, L1));
            return new Point(x, y);
        }
        public virtual Point TopTail()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(-H / 2, -L2));
            double y = currentY + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(-H / 2, -L2));
            return new Point(x, y);
        }
        public virtual Point BottomTail()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(H / 2, -L2));
            double y = currentY + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(H / 2, -L2));
            return new Point(x, y);
        }
    }
}
