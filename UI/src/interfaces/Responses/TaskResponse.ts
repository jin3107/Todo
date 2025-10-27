import type dayjs from "dayjs";
import type { Tier } from "../../commons";

export default interface TaskResponse {
  id?: string,
  title: string;
  description: string;
  dueDate: dayjs.Dayjs;
  isCompleted: boolean;
  priority: Tier;
  completedOn: dayjs.Dayjs;

  createdOn?: dayjs.Dayjs;
  // createdBy?: string;
  modifiedOn?: dayjs.Dayjs;
}