// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';


// User Database
var one_year_month_labels =  ["Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var one_year_week_labels =  ["Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var one_year_day_labels = ["Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

var one_year_month_data = [10000, 30162, 26263, 18394, 18287, 28682, 31274, 33259, 25849, 24159, 32651, 31984];
var one_year_week_data = ["Jun", "Mar 2", "Mar 3", "Mar 4", "Mar 5", "Mar 6", "Mar 7", "Mar 8", "Mar 9", "Mar 10", "Mar 11", "Mar 12", "Mar 13"];
var one_year_day_data = ["Jun", "Mar 2", "Mar 3", "Mar 4", "Mar 5", "Mar 6", "Mar 7", "Mar 8", "Mar 9", "Mar 10", "Mar 11", "Mar 12", "Mar 13"];

var two_year_month_labels = ["Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var two_year_week_labels = ["Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var two_year_day_labels = ["Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jun", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

var target_labels = one_year_month_labels;
var target_datas = one_year_month_data;

// Area Chart Example
var ctx = document.getElementById("AreaChart_main");
var myLineChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: target_labels,
        datasets: [{
            label: "Sessions",
            lineTension: 0.3,
            backgroundColor: "rgba(2,117,216,0.2)",
            borderColor: "rgba(2,117,216,1)",
            pointRadius: 5,
            pointBackgroundColor: "rgba(2,117,216,1)",
            pointBorderColor: "rgba(255,255,255,0.8)",
            pointHoverRadius: 5, 
            pointHoverBackgroundColor: "rgba(2,117,216,1)",
            pointHitRadius: 5, // 點的滑鼠接近感測半徑
            pointBorderWidth: 2,
            data: target_datas,
        }],
    },
    options: {
        scales: {
            xAxes: [{
                time: {
                    unit: 'month'
                },
                gridLines: {
                    display: false
                },
                ticks: {
                    maxTicksLimit: target_labels.length // 設定底部標籤數量 等同資料量
                }
            }],
            yAxes: [{
                ticks: {
                    min: 0,
                    max: 60000,
                    maxTicksLimit: 8
                },
                gridLines: {
                    color: "rgba(0, 0, 0, .125)",
                }
            }],
        },
        legend: {
            display: false
        }
    }
});
