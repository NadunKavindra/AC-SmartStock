import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserAddEditComponent } from '../user-add-edit/user-add-edit.component';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {

  // constructor(private _dialog: MatDialog) { }

  // userAddEdit() {
  //   this._dialog.open(UserAddEditComponent);
  // }
}
