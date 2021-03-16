#include "MyForm.h"
#include <Windows.h>
//using namespace Doberysgame; 

int WINAPI WinMain(HINSTANCE, HINSTANCE, LPSTR, int) 
{
	Doberysgame::Application::EnableVisualStyles();
	Doberysgame::Application::SetCompatibleTextRenderingDefault(false);
	Doberysgame::Application::Run(gcnew Doberysgame::MyForm);
	return 0;
}
