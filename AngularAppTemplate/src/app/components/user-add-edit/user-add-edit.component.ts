import { Component } from '@angular/core';
import { MatDialogModule } from "@angular/material/dialog";
import { MatButton } from "@angular/material/button";
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { NgFor } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogRef } from '@angular/cdk/dialog';
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-user-add-edit',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButton,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatRadioModule,
    MatSelectModule,
    NgFor,
    ReactiveFormsModule
  ],
  templateUrl: './user-add-edit.component.html',
  styleUrl: './user-add-edit.component.scss'
})
export class UserAddEditComponent {
  userForm: FormGroup;

  usertypes: string[] = [
    'Super Admin',
    'Admin',
    'User',
    'Guest'
  ]


  constructor(
    private _fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _dialogRef: DialogRef,
    private _userService: UserService) {

    this.userForm = this._fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      dbo: '',
      gender: '',
      userType: ''
    })
  }

  openSnackBar(message: string, action: string, panelClass: string) {
    this._snackBar.open(message, action, {
      duration: 8000,
      panelClass: [panelClass],
      horizontalPosition: "center",
      verticalPosition: "top"
    });
  }

  onUserSubmit() {
    if (this.userForm.valid) {
      this.openSnackBar("User saved", "Dismiss", "snackbar-success");
      console.log(this.userForm.value);
    }
    else {
      this.openSnackBar("Please enter all required fields", "OK", "snackbar-error");
    }
  }

  closeForm() {
    this._dialogRef.close();

    this._userService.getLogs().subscribe({
      next: (data) => {
        console.log(data);
        //this.contactsDataArray = data;
        //this.dataSource = new MatTableDataSource<Contact>(this.contactsDataArray);
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('Data loaded successfully');
      }
    });
  }

}
