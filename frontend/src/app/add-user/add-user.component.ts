import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../model/user';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {
  id: number = 0;
  userform: FormGroup;
  //@Input() selectedUser: User | undefined;
  //newUser: User | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService
  ) {
    this.userform = this.fb.group({
      name: ['', [Validators.required]],
      lastname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
      username: ['', [Validators.required]],
      role: ['', [Validators.required]],
      id: [0, [Validators.required]]
    });

    //change validators and use fluent validation
  }



  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      if (params['id'] != null) {
        this.userform.get('Id')?.setValue(params['id']);
        const data = this.userService.getUsersById(this.id).subscribe(() => {
          //if (this.userform.valid) {
            if (data) {
              this.userform.setValue(data);//((User)data); //this.selectedUser
              /*this.userform.setValue({
                name: this.selectedUser?.name,
                lastname: this.selectedUser?.lastName,
                email: this.selectedUser?.email,
                username: this.selectedUser?.userName,
                role: this.selectedUser?.role
              });*/
            }
          //}
        });
      }
    });
  }

  save() {
    if (this.userform.invalid)
      return

    if (this.userform.get('id')?.value === 0) {
      this.userService.addUser(this.userform.value).subscribe(() => {
        if (this.userform.valid) {
          console.log("User added");
        }

        this.router.navigate(['/user']);
      });
    } else {
      this.userService.updateUser(this.userform.value).subscribe(() => {
        console.log('usuario actualizado');
      });
    }
  }

}
