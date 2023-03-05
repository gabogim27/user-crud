import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../model/user';
import { UserValidator } from '../validator/user-validator';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {
  id: number = 0;
  userform: FormGroup;
  errors : string[] = [];
  validator: UserValidator = new UserValidator();

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
  }



  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      if (params['id'] != null) {
        this.userform.get('Id')?.setValue(params['id']);
        this.userService.getUsersById(this.id).subscribe((user) => {
            if (user) {
              this.userform.controls['id'].setValue(user.id);
              this.userform.controls['name'].setValue(user.name);
              this.userform.controls['lastname'].setValue(user.lastname);
              this.userform.controls['email'].setValue(user.email);
              this.userform.controls['username'].setValue(user.username);
              this.userform.controls['role'].setValue(user.role);
            }
        });
      }
    });
  }

  reset() {
    this.errors = [];
    this.userform.controls['id'].setValue('');
    this.userform.controls['name'].setValue('');
    this.userform.controls['lastname'].setValue('');
    this.userform.controls['email'].setValue('');
    this.userform.controls['username'].setValue('');
    this.userform.controls['role'].setValue('');
  }

  save() {
    this.errors = [];
    const result = this.validator.validate(this.userform.value);

      if (result.name != null) {

        this.errors.push(result.name);
      }

      if (result.lastname != null) {

        this.errors.push(result.lastname);
      }

      if (result.email != null){
        this.errors.push(result.email);
      }

      if (result.username != null) {

        this.errors.push(result.username);
      }

      if (result.role != null) {

        this.errors.push(result.role);
      }

      if (this.errors.length > 0)
      {
        return;
      }


    if (this.userform.get('id')?.value === 0) {
      this.userService.addUser(this.userform.value).subscribe(() => {
        if (this.userform.valid) {
          console.log("User added");
        }

        this.router.navigate(['/user']);
      });
    } else {
      this.userService.updateUser(this.userform.value).subscribe(() => {
        this.router.navigate(['/user']);
      });
    }
  }

}
