import { Component, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-trend-chart',
  templateUrl: './trend-chart.component.html',
})
export class TrendChartComponent {
  @Input() trend: any;

  chartOptions = {
    responsive: true,
    plugins: {
      legend: {
        labels: {
          color: 'white',
        },
      },
    },
    scales: {
      x: {
        ticks: { color: 'white' },
        grid: { color: 'rgba(255, 255, 255)' },
      },
      y: {
        ticks: { color: 'white' },
        grid: { color: 'rgba(255,255,255)' },
      },
    },
  };
}
