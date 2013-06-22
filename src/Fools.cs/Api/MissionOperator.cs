using Fools.cs.Utilities;
using Fools.cs.builtins;

namespace Fools.cs.Api
{
	public interface MissionOperator
	{
		void execute([NotNull] SinglePartMission mission);
		void execute([NotNull] SequentialMission mission);
	}
}