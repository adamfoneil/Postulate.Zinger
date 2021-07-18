using Zinger.Forms;

namespace Zinger.Models
{
    internal class QuerySource
    {        
        public QuerySource(frmQuery queryForm)
        {
            QueryForm = queryForm;
        }

        public frmQuery QueryForm { get; }

        public override string ToString() => QueryForm.Text;
       
    }
}
