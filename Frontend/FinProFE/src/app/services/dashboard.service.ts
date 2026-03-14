import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryDistribution } from '../models/category-distribution.model';
import { Trend } from '../models/trend.model';
import { Summary } from '../models/summary.model';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private api = 'http://localhost:5172/api/dashboard';
  constructor(private http: HttpClient) {}

  getSummary(startDate: string, endDate: string) {
    return this.http.get<Summary>(`${this.api}/summary`, {
      params: {
        startDate: startDate,
        endDate: endDate,
      },
    });
  }

  getCategoryDistribution(startDate: string, endDate: string) {
    return this.http.get<CategoryDistribution[]>(`${this.api}/categories`, {
      params: {
        startDate: startDate,
        endDate: endDate,
      },
    });
  }

  getTrend(startDate: string, endDate: string) {
    return this.http.get<Trend[]>(`${this.api}/trend`, {
      params: {
        startDate: startDate,
        endDate: endDate,
      },
    });
  }
}
