import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Photo } from 'src/app/_models/Photo';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css']
})
export class PhotoManagementComponent implements OnInit {

  photos: Photo[] = [];

  constructor(private adminService: AdminService ,  private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getPhotosForAproval();
  }

  getPhotosForAproval(){
    this.adminService.getPhotosForAproval().subscribe(response =>
      {
        this.photos = response;
      }
    )
  }

  approvePhoto(photoId){
    this.adminService.approvePhoto(photoId).subscribe(() => {
      //this.photos[photoId].isApproved == true;
      this.photos.splice(this.photos.findIndex(p => p.id === photoId), 1);
    })
  }

  rejectPhoto(photoId) {
    this.adminService.rejectPhoto(photoId).subscribe(() => {
      this.photos.splice(this.photos.findIndex(p => p.id === photoId), 1);
    })
  }

}
