import React, { createContext, useState, useContext, type ReactNode } from 'react';
import type { Student } from '../types/StudentTypes';

interface ModalContextType {
    isOpen: boolean;
    selectedStudent: Student | null;
    openModal: (student?: Student) => void;
    closeModal: () => void;
}

const ModalContext = createContext<ModalContextType | undefined>(undefined);

export const ModalProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);

    const openModal = (student?: Student) => {
        if (student) {
            setSelectedStudent(student);
        } else {
            setSelectedStudent(null);
        }
        setIsOpen(true);
    };

    const closeModal = () => {
        setIsOpen(false);
        setSelectedStudent(null);
    };

    return (
        <ModalContext.Provider value={{ isOpen, selectedStudent, openModal, closeModal }}>
            {children}
        </ModalContext.Provider>
    );
};

export const useModal = () => {
    const context = useContext(ModalContext);
    if (!context) {
        throw new Error('useModal must be used within a ModalProvider');
    }
    return context;
};