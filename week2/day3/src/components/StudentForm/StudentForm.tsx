import React, { useState, useEffect } from 'react';
import { useModal } from '../../contexts/StudentFormModalContext';
import type { CreateStudentForm } from '../../types/StudentTypes';
import { useAddStudent } from '../../hooks/useAddStudent';
import { useUpdateStudent } from '../../hooks/useUpdateStudent';
import './StudentForm.scss';

const StudentForm: React.FC = () => {
    const { isOpen, selectedStudent, closeModal } = useModal();
    const { mutate: addStudent } = useAddStudent();
    const { mutate: updateStudent } = useUpdateStudent();

    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        major: '',
        age: '',
        courses: ''
    });

    useEffect(() => {
        if (selectedStudent) {
            const student = selectedStudent as any;
            setFormData({
                firstName: student.firstName || student.FirstName || '',
                lastName: student.lastName || student.LastName || '',
                email: student.email || student.Email || '',
                major: student.major || student.Major || '',
                age: String(student.age || student.Age || ''),
                courses: student.courses?.join(', ') || student.Courses?.join(', ') || ''
            });
        } else {
            setFormData({ firstName: '', lastName: '', email: '', major: '', age: '', courses: '' });
        }
    }, [selectedStudent, isOpen]);

    if (!isOpen) return null;

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const studentData: CreateStudentForm = {
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: formData.email,
            major: formData.major,
            age: Number(formData.age),
            courses: formData.courses.split(',').map(c => c.trim()).filter(c => c !== '')
        };

        const student = selectedStudent as any;
        const studentId = student?.id || student?.Id;

        if (studentId) {
            updateStudent({ id: studentId, data: studentData }, {
                onSuccess: () => closeModal()
            });
        } else {
            addStudent(studentData, {
                onSuccess: () => closeModal()
            });
        }
    };

    return (
        <div className="modal-overlay" onClick={closeModal}>
            <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                <div className="modal-header">
                    <h3>{selectedStudent ? 'Edit Student' : 'Add New Student'}</h3>
                    <button className="close-btn" onClick={closeModal}>&times;</button>
                </div>

                <form onSubmit={handleSubmit} className="student-form">
                    <div className="form-group">
                        <label>First Name</label>
                        <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} required />
                    </div>

                    <div className="form-group">
                        <label>Last Name</label>
                        <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} required />
                    </div>

                    <div className="form-group">
                        <label>Email</label>
                        <input type="email" name="email" value={formData.email} onChange={handleChange} required />
                    </div>

                    <div className="form-row">
                        <div className="form-group">
                            <label>Age</label>
                            <input type="number" name="age" value={formData.age} onChange={handleChange} required />
                        </div>
                        <div className="form-group">
                            <label>Major</label>
                            <input type="text" name="major" value={formData.major} onChange={handleChange} required />
                        </div>
                    </div>

                    <div className="form-group">
                        <label>Courses (Separated by comma ,)</label>
                        <input type="text" name="courses" value={formData.courses} onChange={handleChange} placeholder="e.g. Math, Databases, OOP" />
                    </div>

                    <div className="form-actions">
                        <button type="button" className="btn-cancel" onClick={closeModal}>Cancel</button>
                        <button type="submit" className="btn-submit">{selectedStudent ? 'Update Student' : 'Save Student'}</button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default StudentForm;