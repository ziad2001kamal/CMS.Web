using CMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetAll();
        Task<List<PieChartViewModel>> GetUserTypeChart();
        Task<List<PieChartViewModel>> GetContentTypeChart();
        Task<List<PieChartViewModel>> GetContentByMonthChart();



    }
}
