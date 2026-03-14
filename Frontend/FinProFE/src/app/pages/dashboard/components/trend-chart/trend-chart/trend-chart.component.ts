import { Component, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-trend-chart',
  templateUrl: './trend-chart.component.html',
})
export class TrendChartComponent {
  @Input() trend: any;
}
