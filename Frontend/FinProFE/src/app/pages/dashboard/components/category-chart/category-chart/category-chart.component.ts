import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-category-chart',
  templateUrl: './category-chart.component.html',
  styleUrl: './category-chart.component.css',
})
export class CategoryChartComponent {
  @Input() categories: any;
}
