import { useQuery } from '@tanstack/react-query';
import axiosInstance from '../api/axiosInstance';
import type { Student } from '../types/StudentTypes';

const fetchStudents = async (): Promise<Student[]> => {
    const response = await axiosInstance.get('/Students');

    console.log("Full Backend Response:", response.data);

    const raw = response.data;

    if (Array.isArray(raw)) return raw;

    if (raw?.result && Array.isArray(raw.result)) return raw.result;

    if (raw?.result?.result && Array.isArray(raw.result.result)) return raw.result.result;

    const data = raw?.result || raw;
    return data?.items || data?.data || data?.$values || [];
};

export const useGetStudents = () => {
    return useQuery<Student[]>({
        queryKey: ['students'],
        queryFn: fetchStudents,
        staleTime: 1000 * 60 * 5,
    });
};