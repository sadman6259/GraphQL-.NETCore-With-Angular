export type Course = {
    id: number;
    participantId: number;
    participantName: string;
   
}

export type Query = {
    participants: Course[];
}