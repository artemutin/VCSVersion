﻿using System;

namespace VCSVersion.VCS
{
    /// <summary>
    /// Abstraction for a repository log query builder.
    /// </summary>
    public interface ILogQueryBuilder
    {
        /// <summary>
        /// Returns a <see cref="ILogQuery"/> that selects a commit based on its unique hash number.
        /// </summary>
        /// <param name="hash">The commit unique hash.</param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="hash"/> is <c>null</c> or empty.</para>
        /// </exception>
        ILogQuery Single(string hash);

        /// <summary>
        /// Creates a <see cref="HgLogQuery"/> that finds commits that belong to the named branch.
        /// </summary>
        /// <param name="name">Branch name.</param>
        ILogQuery ByBranch(string name);

        /// <summary>
        /// Create a <see cref="ILogQuery"/> that includes the commit
        /// specified and all ancestor commits.
        /// </summary>
        /// <param name="hash">The commit hash to end with.</param>
        ILogQuery AncestorsOf(string hash);
    }
}