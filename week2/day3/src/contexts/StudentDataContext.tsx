import React, { createContext, useState, useContext, type ReactNode } from 'react';
import type { Student, CreateStudentForm } from '../types/StudentTypes';

interface StudentDataContextType {
    students: Student[];
    addStudent: (student: CreateStudentForm) => void;
    deleteStudent: (id: string) => void;
}

const StudentDataContext = createContext<StudentDataContextType | undefined>(undefined);

export const StudentProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [students, setStudents] = useState<Student[]>([
        { id: '1', firstName: 'أحمد', lastName: 'محمد', email: 'ahmad@example.com', age: 22, major: 'هندسة البرمجيات', courses: ['قواعد البيانات', 'برمجة الويب'] },
        { id: '2', firstName: 'سارة', lastName: 'علي', email: 'sara@example.com', age: 21, major: 'الذكاء الاصطناعي', courses: ['تعلم الآلة', 'خوارزميات'] }
    ]);

    const addStudent = (newStudent: CreateStudentForm) => {
        const student: Student = {
            ...newStudent,
            id: Math.random().toString(36).substring(2, 9)
        };
        setStudents([...students, student]);
    };

    const deleteStudent = (id: string) => {
        setStudents(students.filter(s => s.id !== id));
    };

    return (
        <StudentDataContext.Provider value={{ students, addStudent, deleteStudent }}>
            {children}
        </StudentDataContext.Provider>
    );
};

export const useStudentData = () => {
    const context = useContext(StudentDataContext);
    if (!context) {
        throw new Error('useStudentData must be used within a StudentProvider');
    }
    return context;
};