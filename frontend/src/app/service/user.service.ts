import { Injectable } from '@angular/core';
import { User } from '../model/user';
import { HttpClient } from "@angular/common/http";
import { Observable, ReplaySubject } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;
  private currentUsersource = new ReplaySubject<User>(1);
  /*private userList: User[] = [{
    id: 1,
    name: 'Gabriel',
    lastname: 'Gimenez',
    email: 'gabito@gmail.com',
    username: 'Male',
    password: '8978786933'
}];*/

constructor(private http: HttpClient) {}

getUsers(): Observable<User[]> {
  return this.http.get<User[]>(this.baseUrl + `api/user`).pipe(
    map((users) => {
      return users;
    })
  );
}

/*getUsers() {
    return this.userList
}*/

/*getUsersByID(id: number) {
    return this.userList.find(x => x.id == id)
}*/

getUsersById(id: number): Observable<User> {
  return this.http.get<User>(this.baseUrl + `api/user/` + id).pipe(
    map((user) => {
      return user;
    })
  );
}

/*addUser(user: User) {
    user.id = new Date().getTime();
    this.userList.push(user);
}*/

/*addUser(user: User): Observable<User> {
  return this.http.post<User>(this.baseUrl + `api/user/AddUser`, user).pipe(
    map((user) => {
      return user;
    })
  );
}*/

addUser(user: User) : Observable<User> {
  return this.http.post<User>(this.baseUrl + 'api/user/AddUser', user);
}

/*updateUser(user: User) {
    const userIndex = this.userList.findIndex(x => x.id == user.id);
    if (userIndex != null && userIndex != undefined) {
        this.userList[userIndex] = user;
    }
}*/

updateUser(user: User): Observable<User> {
  return this.http.put<User>(this.baseUrl + `api/user/UpdateUser`, user).pipe(
    map((user) => {
      return user;
    })
  );
}

/*removeUser(id: number) {
  this.userList = this.userList.filter(x => x.id != id);
}*/

removeUser(id: number) {
  return this.http.delete(this.baseUrl + `api/user/` + id);
}

}
