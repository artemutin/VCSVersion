﻿using System.Globalization;
using VCSVersion.AssemblyVersioning;
using VCSVersion.Configuration;
using VCSVersion.SemanticVersions;

namespace VCSVersion.Output
{
    public sealed class VersionVariablesBuilder
    {
        private SemanticVersion _version;
        private EffectiveConfiguration _config;
        
        public string Major => _version.Major.ToString();
        public string Minor => _version.Minor.ToString();
        public string Patch => _version.Patch.ToString();
        
        public string PreReleaseTag => _version.PreReleaseTag;
        public string PreReleaseTagWithDash => _version.PreReleaseTag.IsNull() ? null : "-" + _version.PreReleaseTag;
        public string PreReleaseLabel => _version.PreReleaseTag.IsNull() ? null : _version.PreReleaseTag.Name;
        public string PreReleaseNumber => _version.PreReleaseTag.IsNull() ? null : _version.PreReleaseTag.Number.ToString();

        public string BuildMetadata => _version.BuildMetadata;
        public string BuildMetadataPadded => _version.BuildMetadata.ToString("p" + _config.BuildMetaDataPadding);
        public string FullBuildMetadata => _version.BuildMetadata.ToString("f");

        public string BranchName => _version.BuildMetadata.Branch;
        public string Sha => _version.BuildMetadata.Hash;

        public string CommitDate => 
            _version.BuildMetadata
            .CommitDate
            .UtcDateTime
            .ToString(_config.CommitDateFormat, CultureInfo.InvariantCulture);

        public string CommitsSinceVersionSource => 
            _version.BuildMetadata
            .CommitsSinceVersionSource
            .ToString(CultureInfo.InvariantCulture);

        public string CommitsSinceVersionSourcePadded => 
            _version.BuildMetadata
            .CommitsSinceVersionSource
            .ToString(CultureInfo.InvariantCulture)
            .PadLeft(_config.CommitsSinceVersionSourcePadding, '0');

        public string AssemblySemVer => _version.GetAssemblyVersion(_config.AssemblyVersioningScheme);
        public string AssemblyFileSemVer => _version.GetAssemblyFileVersion(_config.AssemblyFileVersioningScheme);

        public string MajorMinorPatch => $"{_version.Major}.{_version.Minor}.{_version.Patch}";
        public string SemVer => _version.ToString();
        public string FullSemVer => _version.ToString("f");
        public string DefaultInformationalVersion => _version.ToString("i");
        
        public string NuGetVersion => _version.ToString("t");
        public string NuGetPreReleaseTag => _version.PreReleaseTag.IsNull() ? null : _version.PreReleaseTag.ToString("t").ToLower();
        
        public VersionVariablesBuilder(SemanticVersion version, EffectiveConfiguration config)
        {
            _version = version;
            _config = config;
        }

        public VersionVariables Build()
        {
            return new VersionVariables
            {
                Major = Major,
                Minor = Minor,
                Patch = Patch,
                BuildMetadata = BuildMetadata,
                BuildMetadataPadded = BuildMetadataPadded,
                FullBuildMetadata = FullBuildMetadata,
                BranchName = BranchName,
                Sha = Sha,
                MajorMinorPatch = MajorMinorPatch,
                SemVer = SemVer,
                FullSemVer = FullSemVer,
                AssemblySemVer = AssemblySemVer,
                AssemblyFileSemVer = AssemblyFileSemVer,
                PreReleaseTag = PreReleaseTag,
                PreReleaseTagWithDash = PreReleaseTagWithDash,
                PreReleaseLabel = PreReleaseLabel,
                PreReleaseNumber = PreReleaseNumber,
                InformationalVersion = DefaultInformationalVersion,
                CommitDate = CommitDate,
                NuGetVersion = NuGetVersion,
                NuGetPreReleaseTag = NuGetPreReleaseTag,
                CommitsSinceVersionSource = CommitsSinceVersionSource
            };
        }
    }
}