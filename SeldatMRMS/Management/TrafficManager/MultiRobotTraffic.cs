using SeldatMRMS.Communication;
using System;

namespace SeldatMRMS.Management.TrafficManager
{
    public class MultiRobotTraffic
	{
		public struct ParamsRosSocket
		{
			public int publication_servertoMultirules;
			public int publication_trafficInfoRequest;
		}
		
		private void multirulestoserverHandler(Communication.Message message)
		{
			StandardString standardString = (StandardString)message;
			String data = standardString.data;
		}
		private void trafficInfoResponseHandler(Communication.Message message)
		{
			StandardString standardString = (StandardString)message;
			String data = standardString.data;
		}
	}
}
