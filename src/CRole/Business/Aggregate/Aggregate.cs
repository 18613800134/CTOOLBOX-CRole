
namespace CRole.Business.Aggregate
{
    using CAM.Core.Business.Interface;
    using CAM.Core.Business.Aggregate;
    using DBContext;

    public partial class Aggregate : BaseAggregate, IBaseInterfaceCommand<DBContextCRole>
    {
        public Aggregate()
        {
            this.dbContext = new DBContextCRole();
        }

        public DBContextCRole DBContext
        {
            get { return (DBContextCRole)this.dbContext; }
        }
    }
}
