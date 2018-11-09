using SeldatMRMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeldatMRMS.DBProcedureService;

namespace SeldatMRMS
{
    public class RegisterProcedureService
    {
        protected virtual bool Cancel() { return false; }
        public class RegisterProcedureItem
        {
            public ProcedureControlServices items;
            public ProcedureDataItems ProcedureDataItems;
            public RobotAgent robot;
        }
        protected List<RegisterProcedureItem> RegisterProcedureItemList=new List<RegisterProcedureItem>();
        public RegisterProcedureService() {}
        public String GetAIDProcedure()
        {
            return "1234";
        }
        public enum ProcedureItemSelected
        {
            PROCEDURE_FORLIFT_TO_BUFFER,
            PROCEDURE_BUFFER_TO_MACHINE,
            PROCEDURE_BUFFER_TO_HOPPER,
        }
    }
}
