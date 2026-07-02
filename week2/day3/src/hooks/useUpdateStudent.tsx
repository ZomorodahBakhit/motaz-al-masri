import { useMutation, useQueryClient } from '@tanstack/react-query';
import axiosInstance from '../api/axiosInstance';
import type { CreateStudentForm } from '../types/StudentTypes';

interface UpdateStudentParams {
    id: string;
    data: CreateStudentForm;
}

const updateStudent = async ({ id, data }: UpdateStudentParams) => {
    const response = await axiosInstance.put(`/Students/${id}`, data);
    return response.data.result || response.data;
};

export const useUpdateStudent = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: updateStudent,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['students'] });
        },
    });
};
