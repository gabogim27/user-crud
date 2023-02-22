import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserService } from '../service/user.service';
import { User } from '../model/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  userList: User[] = [];
  @Output() onSelected = new EventEmitter<any>();
    first = 0;
    rows = 10;
    constructor(
      private userService: UserService
      ) {}
      ngOnInit() {
        this.get();
      }

  onSelectedUser(user: any) {
    console.log(user);
    this.onSelected.emit(user);
  }

  next() {
      this.first = this.first + this.rows;
  }
  prev() {
      this.first = this.first - this.rows;
  }
  reset() {
      this.first = 0;
  }

  isLastPage(): boolean {
      return this.userList ? this.first === (this.userList.length - this.rows) : true;
  }

  isFirstPage(): boolean {
      return this.userList ? this.first === 0 : true;
  }

  get() {
    this.userService.getUsers().subscribe((data) => {
      this.userList = data;
    });
  }

  remove(id: number) {
    this.userService.removeUser(id).subscribe(() => {
      //console.log("User with id " + id + "removed");
      this.get();
    });
      //this.userService.removeUser(id);
  }
}
