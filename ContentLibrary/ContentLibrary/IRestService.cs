namespace ContentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IRestService
    {
        string RefreshDataAsync();
    }
}
