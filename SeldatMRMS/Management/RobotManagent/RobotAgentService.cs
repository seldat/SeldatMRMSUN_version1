﻿using SeldatMRMS.Communication;
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
// Three checked points have intersection : TopHeader/ Middle Header / Bottom Header
// These three points must contact to the risk areas
namespace SeldatMRMS.Management
{
    public class PriorityLevel
    {
        public PriorityLevel()
        {
            this.OnMainRoad = 0;
            this.OnAuthorizedPriorityProcedure = false;
        }
        public int OnMainRoad { get; set; } //  Index on Road;
        public bool OnAuthorizedPriorityProcedure { get; set; }

    }
    public class RobotAgentService
    {
        
        public event Action<Communication.Message> ZoneHandler;
        public event Action<Communication.Message> AmclPoseHandler;
        public event Action<Communication.Message> FinishStatesHandler;
        public double RealWidth { get; set; }
        public double RealHeigth { get; set; }
        public double currentX;
        public double currentY;
        public double Angle;
        public double L1;
        public double L2;
        public double H;
        public PriorityLevel registePriorityLevel;
        public RobotAgentService()
        {
            registePriorityLevel = new PriorityLevel();
        }
        public virtual Point TopHeader()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(H / 2, L1));
            double y = currentY+ Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(H / 2, L1));
            return new Point(x, y);
        }
        public virtual Point MiddleHeader()
        {
            return new Point((TopHeader().X+BottomHeader().X)/2, (TopHeader().Y + BottomHeader().Y) / 2);
        }
        public virtual Point BottomHeader()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(-H / 2, L1));
            double y = currentY + Math.Sqrt(Math.Abs(L1) * Math.Abs(L1) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(-H / 2, L1));
            return new Point(x, y);
        }
        public virtual Point LeftSide()
        {
            return new Point((TopHeader().X+TopTail().X)/2, (TopHeader().Y + TopTail().Y) / 2);
        }
        public virtual Point TopTail()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(-H / 2, -L2));
            double y = currentY + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(-H / 2, -L2));
            return new Point(x, y);
        }
        public virtual Point MiddleTail()
        {
            return new Point((TopTail().X+BottomTail().X)/2, (TopTail().Y + BottomTail().Y) / 2);
        }
        public virtual Point BottomTail()
        {
            double x = currentX + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Cos(Angle + Math.Atan2(H / 2, -L2));
            double y = currentY + Math.Sqrt(Math.Abs(L2) * Math.Abs(L2) + Math.Abs(H / 2) * Math.Abs(H / 2)) * Math.Sin(Angle + Math.Atan2(H / 2, -L2));
            return new Point(x, y);
        }
        public virtual Point RightSide()
        {
            return new Point((BottomHeader().X+BottomTail().X)/2, (BottomHeader().Y + BottomTail().Y) / 2);
        }

        public Point[] RiskAreaHeader()  // From Point : TopHeader / BottomHeader / RigtSide // LeftSide
        {
            return new Point[4] { TopHeader(), BottomHeader(), RightSide(), LeftSide() };
        }
        public Point[] RiskAreaTail()  // From Point : TopTail / BottomTail / RigtSide // LeftSide
        {
            return new Point[4] { TopTail(), BottomTail(), RightSide(), LeftSide() };
        }

        public Point[] RiskAreaRightSide()  // From Point : TopHeader / TopTail / Middle TAil //Middle HEader
        {
            return new Point[4] { TopHeader(), TopTail(), MiddleTail(), MiddleHeader() };
        }
        public Point[] RiskAreaLeftSide()  // From Point : BOttom Header / Bottom Tail / Middle TAil //Middle HEader
        {
            return new Point[4] { BottomHeader(), BottomTail(), MiddleTail(), MiddleHeader() };
        }
        public Point[] FullRiskArea()
        {
            return new Point[4] { TopHeader(), BottomHeader(), TopTail(), BottomTail() };
        }

    }
}
