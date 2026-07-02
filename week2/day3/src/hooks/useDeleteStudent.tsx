import { useMutation, useQueryClient } from '@tanstack/react-query';
import axiosInstance from '../api/axiosInstance';

const deleteStudent = async (id: string) => {
    await axiosInstance.delete(`/Students/${id}`);
};

export const useDeleteStudent = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: deleteStudent,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['students'] });
        },
    });
};
