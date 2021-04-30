import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import gql from 'graphql-tag';

import { Employee, Query } from '../types';
const EMPLOYEES = gql`
query employeesattendence ($attendenceDate: String!){
  employeesattendence (attendenceDate: $attendenceDate){
            employeeId,
            employeeName
          
         
      }
  }
`;


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})


export class ListComponent implements OnInit {
  employees: Observable<Employee[]>;
  allemployees:Employee[] = [];
  dateinput: String = "";
  message:string="";


  constructor(private apollo: Apollo) { }

  ngOnInit() {
    this.message ="please select a date to view Employees";

//this.getEmployee();
  }
  getEmployee(data){
    // this.message ="No record found on this date";
    debugger
    this.apollo.watchQuery<any>({
       query: EMPLOYEES,
       variables:{
        attendenceDate: data
       }
     })
     .valueChanges
     .subscribe(({data, loading}) => {
       console.log(data.employeesattendence);
       this.allemployees = data.employeesattendence;
     });
   }
 
}