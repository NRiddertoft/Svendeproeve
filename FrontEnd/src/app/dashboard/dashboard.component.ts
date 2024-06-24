import { Component, OnInit } from '@angular/core';
import { AuthService } from '../app.routes';
import * as mockData from '../../assets/mock-data.json';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  role: string | null = null;
  data: any[] = (mockData as any).default;
  uniqueStudios: string[] = [];
  uniqueJobTitles: string[] = [];
  uniqueProjects: string[] = [];
  uniqueExpertise: string[] = [];

  constructor(private authService: AuthService) {}

  ngOnInit() {
    const currentUser = this.authService.getCurrentUserValue();
    if (currentUser && currentUser.role) {
      this.role = currentUser.role;
    }

    // Remove duplicates
    this.uniqueStudios = [...new Set(this.data.map((person) => person.Studio))];
    this.uniqueJobTitles = [
      ...new Set(this.data.map((person) => person.JobTitle)),
    ];
    this.uniqueProjects = [
      ...new Set(this.data.flatMap((person) => person.Projects)),
    ];
    this.uniqueExpertise = [
      ...new Set(this.data.map((person) => person.Expertise)),
    ];
  }

  logout() {
    console.log('Logout clicked');
    this.authService.logout();
  }

  editContent() {
    console.log('Edit content clicked');
  }
}
