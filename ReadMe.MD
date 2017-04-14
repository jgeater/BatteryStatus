BatteryStatus

runs untill battery is at 60% then exits

This is designed for use in imaging to solve two problems. 


1:Make sure the battery has enough charge to finish the imaging process. 
2:Systems with UEFI firmware can install Firmware updates as part of imaging, but these updates will fail if the battery is below 40% charge

This application runs during imaging while in Win PE and will hold the TS untill the the battery is at 60% or higher. If the battery is already at 60% or higher it will exit.

.NET has top be included in WinPE for this to work.