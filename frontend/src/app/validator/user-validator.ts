import { Validator } from 'fluentvalidation-ts';
import { User } from '../model/user';

export class UserValidator extends Validator<User>{
  constructor() {
    super();

    this.ruleFor('name')
      .notEmpty()
      .withMessage('Please enter your name');

    this.ruleFor('lastname')
    .notEmpty()
    .withMessage('Please enter your lastname');

    this.ruleFor('email')
    .notEmpty().withMessage('Please enter your email')
    .emailAddress()
    .withMessage('E-mail not in correct format');

    this.ruleFor('username')
    .notEmpty()
    .withMessage('Please enter your username');

    this.ruleFor('role')
    .notEmpty()
    .withMessage('Please enter your role');
  }
}
