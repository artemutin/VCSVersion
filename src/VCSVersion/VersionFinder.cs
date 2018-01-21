using System;
using System.Collections.Generic;
using System.Text;
using VCSVersion.SemanticVersions;
using VCSVersion.VersionCalculation;
using VCSVersion.VersionCalculation.BaseVersionCalculation;

namespace VCSVersion
{
    public class VersionFinder
    {
        private readonly IBaseVersionCalculator baseVersionCalculator;
        private readonly IMetadataCalculator metadataCalculator;
        private readonly IPreReleaseTagCalculator tagCalculator;

        public VersionFinder(
            IBaseVersionCalculator baseVersionCalculator = null,
            IMetadataCalculator metadataCalculator = null,
            IPreReleaseTagCalculator tagCalculator = null)
        {
            this.baseVersionCalculator = baseVersionCalculator;
            this.metadataCalculator = metadataCalculator;
            this.tagCalculator = tagCalculator;
        }

        public SemanticVersion FindVersion(IVersionContext context)
        {
            var branchName = context.CurrentBranch.Name;
            var commit = context.CurrentCommit == null ? "-" : context.CurrentCommit.Hash;
            Logger.WriteInfo($"Running against branch: {branchName} ({commit})");

            if (context.IsCurrentCommitTagged)
            {
                Logger.WriteInfo(
                    $"Current commit is tagged with version {context.CurrentCommitTaggedVersion}, " 
                    + "version calcuation is for metadata only.");
            }

            return new NextVersionCalculator(
                    baseVersionCalculator,
                    metadataCalculator,
                    tagCalculator)
                .CalculateVersion(context);
        }
    }
}
