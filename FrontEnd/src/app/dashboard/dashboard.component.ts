import { Component, OnInit } from '@angular/core';
import { AuthService } from '../app.routes'; // Adjust the path if necessary
import mockData from '../../assets/mock-data.json';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  role: string | null = null;
  data: any[] = mockData;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    const currentUser = this.authService.getCurrentUserValue();
    if (currentUser && currentUser.role) {
      this.role = currentUser.role;
    }
  }

  editContent() {
    console.log('Edit clicked');
  }
  logout() {
    console.log('Logout clicked');
    this.authService.logout();
  }
}
