import React from "react";
import { useNavigate } from "react-router-dom";
import './StudentsList.scss';
import { useModal } from '../../contexts/StudentFormModalContext';
import { useGetStudents } from '../../hooks/useGetStudents';
import { useDeleteStudent } from '../../hooks/useDeleteStudent';
import StudentForm from '../../components/StudentForm/StudentForm';

const StudentsList: React.FC = () => {
    const { data: students, isLoading, isError, error } = useGetStudents();
    const { mutate: deleteStudent } = useDeleteStudent();
    const { openModal } = useModal();
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/login');
    };

    if (isLoading) {
        return <div style={{ textAlign: 'center', padding: '3rem', fontWeight: 'bold' }}>Loading students data...</div>;
    }

    if (isError) {
        return <div style={{ textAlign: 'center', color: '#dc3545', padding: '3rem', fontWeight: 'bold' }}>Error loading data: {(error as Error).message}</div>;
    }

    const studentsArray = Array.isArray(students) ? students : [];

    return (
        <div className="students-list-container">
            <div className="header-section">
                <h2>Students List</h2>
                <div className="header-actions">
                    <button className="btn-primary" onClick={() => openModal()}>Add New Student</button>
                    <button className="btn-danger" onClick={handleLogout} style={{ marginLeft: '10px', backgroundColor: '#dc3545', color: 'white', border: 'none', padding: '10px 15px', borderRadius: '4px', cursor: 'pointer' }}>Logout</button>
                </div>
            </div>

            <table className="students-table">
                <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Age</th>
                    <th>Email</th>
                    <th>Major</th>
                    <th>Courses</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                {studentsArray.map((student) => (
                    <tr key={student.id || Math.random()}>
                        <td>{student.firstName || student.firstName}</td>
                        <td>{student.lastName || student.lastName}</td>
                        <td>{student.age || student.age}</td>
                        <td>{student.email || student.email}</td>
                        <td>{student.major || student.major}</td>
                        <td>{student.courses?.join(', ') || student.courses?.join(', ') || 'No courses'}</td>
                        <td>
                            <button className="btn-edit" onClick={() => openModal(student)}>Edit</button>
                            <button className="btn-delete" onClick={() => deleteStudent(student.id || student.id)}>Delete</button>
                        </td>
                    </tr>
                ))}

                {studentsArray.length === 0 && (
                    <tr>
                        <td colSpan={7} style={{ textAlign: 'center', color: '#666' }}>There are no students yet</td>
                    </tr>
                )}
                </tbody>
            </table>

            <StudentForm />
        </div>
    );
};

export default StudentsList;