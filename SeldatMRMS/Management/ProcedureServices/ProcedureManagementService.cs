using SeldatMRMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeldatMRMS
{
    public class ProcedureManagementService:RegisterProcedureService
    {
        public ProcedureManagementService()
        {

        }
        public void Register(ProcedureItemSelected ProcedureItem,RobotAgent robot)
        {
            switch(ProcedureItem)
            {
                case ProcedureItemSelected.PROCEDURE_FORLIFT_TO_BUFFER:

                    
                    break;
                case ProcedureItemSelected.PROCEDURE_BUFFER_TO_MACHINE: break;
                case ProcedureItemSelected.PROCEDURE_BUFFER_TO_HOPPER: break;
            }
        }
    }
}
