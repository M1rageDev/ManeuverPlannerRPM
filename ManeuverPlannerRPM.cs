using UnityEngine;
using KSP;
using System;

namespace ManeuverPlannerRPM
{
	internal class ManeuverPlannerRPM : InternalModule
	{
		double prograde, normal, radialOut, relTime;
		
		public void SetPrograde(double prograde)
		{
			this.prograde = prograde;
		}

		public void SetNormal(double normal)
		{
			this.normal = normal;
		}

		public void SetRadialOut(double radialOut)
		{
			this.radialOut = radialOut;
		}

		public void SetRelativeTime(double time)
		{
			this.relTime = time;
		}

		public void PlaceManeuver(bool state)
		{
			if (vessel.patchedConicSolver == null)
				throw new Exception(
					"A KSP limitation makes it impossible to access the manuever nodes of this vessel at this time. " +
					"(perhaps it's not the active vessel?)");

			// place the actual node at current time+relative time
			ManeuverNode node = vessel.patchedConicSolver.AddManeuverNode(Planetarium.GetUniversalTime() + relTime);

			// apply the DV values to the maneuver
			node.DeltaV = new Vector3d(radialOut, normal, prograde);

			vessel.patchedConicSolver.UpdateFlightPlan();
		}
	}
}