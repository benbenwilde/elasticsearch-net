﻿using System;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.Core.Xunit
{
	public class SkipOnCiAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";

		public static bool RunningOnTeamCity => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public static bool RunningOnAzureDevops => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD"));
		public static bool RunningOnAppVeyor => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPVEYOR_BUILD_VERSION"));
		public override bool Skip => RunningOnTeamCity || RunningOnAppVeyor || RunningOnAzureDevops;
	}
	//

	//TODO 7.0: this attribute and all its usages have to be scrubbed before we can do a 7.x release
	/// <summary> Indicates test can not run because of a presumed upstream bug </summary>
	public class BlockedByIssueAttribute : SkipTestAttributeBase
	{
		public BlockedByIssueAttribute(string url) => Url = url;

		public override string Reason => $"Blocked temporarily by {Url}";

		public override bool Skip => true;

		private string Url { get; }
	}
}
