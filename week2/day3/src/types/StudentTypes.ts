export interface Student {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  age: number;
  major: string;
  courses: string[];
}

export interface CreateStudentForm{
  firstName: string;
  lastName: string;
  email: string;
  age: number;
  major: string;
  courses: string[];
}
