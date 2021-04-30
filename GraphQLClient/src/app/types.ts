export type Employee = {
    id: number;
    employeeId: number;
    emloyeeName: string;
    attendenceDate:string;
   
}

export type Query = {
    employees: Employee[];
}