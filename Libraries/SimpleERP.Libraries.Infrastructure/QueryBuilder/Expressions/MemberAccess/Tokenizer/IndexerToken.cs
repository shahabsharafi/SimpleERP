// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace SimpleERP.Libraries.Infrastructure.QueryBuilder.Infrastructure.Implementation.Expressions
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using SimpleERP.Libraries.Infrastructure.QueryBuilder.Extensions;

    internal class IndexerToken : IMemberAccessToken
    {
        private readonly ReadOnlyCollection<object> arguments;

        public IndexerToken(IEnumerable<object> arguments)
        {
            this.arguments = arguments.ToReadOnlyCollection();
        }

        public IndexerToken(params object[] arguments) : this((IEnumerable<object>) arguments)
        {
        }

        public ReadOnlyCollection<object> Arguments
        {
            get
            {
                return this.arguments;
            }
        }
    }
}