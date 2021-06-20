{
    function renderData(model) {
        console.log(model)
        if (model === null || model.length === 0) {
            return;
        }
        $('#top-5-chart-div').show();
        $('#all-chart-div').show();

        var labels = [];
        var data = []
        var backgroundColor = []
        model.forEach(function (item, index) {
            labels.push(item.SicCodeDescription)
            var randomColor = Math.floor(Math.random() * 16777215).toString(16);
            data.push(item.ContactCounts)
            backgroundColor.push("#" + randomColor)
        });

        var donutChartCanvas = $('#donutChart').get(0).getContext('2d')
        var donutData = {
            labels: labels.slice(0, 5),
            datasets: [
                {
                    data: data.slice(0, 5),
                    backgroundColor: backgroundColor.slice(0, 5)
                }
            ]
        }
        var donutOptions = {
            maintainAspectRatio: false,
            responsive: true,
        }
        //Create pie or douhnut chart
        // You can switch between pie and douhnut using the method below.
        var donutChart = new Chart(donutChartCanvas, {
            type: 'doughnut',
            data: donutData,
            options: donutOptions
        })


        var barChartCanvas = $('#barChart').get(0).getContext('2d')
        var areaChartData = {
            labels: labels,
            datasets: [
                {
                    label: 'Potential Businesses',
                    backgroundColor: 'rgba(60,141,188,0.9)',
                    borderColor: 'rgba(60,141,188,0.8)',
                    pointRadius: false,
                    pointColor: '#3b8bba',
                    pointStrokeColor: 'rgba(60,141,188,1)',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(60,141,188,1)',
                    data: data
                }
            ]
        }
        var barChartData = jQuery.extend(true, {}, areaChartData)
        var temp0 = areaChartData.datasets[0]
        barChartData.datasets[0] = temp0

        var barChartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            datasetFill: false
        }

        var barChart = new Chart(barChartCanvas, {
            type: 'bar',
            data: barChartData,
            options: barChartOptions
        })
    }
}