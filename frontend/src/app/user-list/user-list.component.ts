import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserService } from '../service/user.service';
import { User } from '../model/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {


  @Input() selectedUser: User | undefined;
  newUser: User | undefined;
  userList: User[] = [];
  @Output() onSelected = new EventEmitter<User>();
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
    this.userService.getUsers().subscribe(
      (data) => {
        if (data) {
          this.userList = data;
        }
    });
  }

  remove(id: number) {
    this.userService.removeUser(id).subscribe(() => {
      this.get();
    });
  }

}
