import { useMutation, useQueryClient } from '@tanstack/react-query';
import axiosInstance from '../api/axiosInstance';
import type { CreateStudentForm } from '../types/StudentTypes';

const addStudent = async (newStudent: CreateStudentForm) => {
    const response = await axiosInstance.post('/Students', newStudent);
    return response.data.result || response.data;
};

export const useAddStudent = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: addStudent,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['students'] });
        },
    });
};
